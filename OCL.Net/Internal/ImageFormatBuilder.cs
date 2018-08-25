using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using OCL.Net.Native.Enums;
using OCL.Net.Native.Structures;

namespace OCL.Net.Internal
{
    public sealed class ImageFormatBuilder
    {
        public Type DataType
        {
            get => _dataType ?? throw new InvalidOperationException("Data type is not defined");
            set => _dataType = value;
        }

        public bool Normalized
        {
            get => _normalized ?? throw new InvalidOperationException("Normalized is not defined");
            set => _normalized = value;
        }

        public ImageChannelOrder ChannelOrder
        {
            get => _channelOrder ?? throw new InvalidOperationException("Channel order is not defined");
            set => _channelOrder = value;
        }

        public ImageChannelType ChannelType
        {
            get => _channelType ?? throw new InvalidOperationException("Channel type is not defined");
            set => _channelType = value;
        }

        private Type _dataType;
        private bool? _normalized;
        private ImageChannelOrder? _channelOrder;
        private ImageChannelType? _channelType;

        public ImageFormatBuilder(Type dataType = null, bool? normalized = null,
            ImageChannelOrder? channelOrder = null, ImageChannelType? channelType = null)
        {
            _dataType = dataType;
            _normalized = normalized;
            _channelOrder = channelOrder;
            _channelType = channelType;
        }

        public ImageFormatBuilder WithDataType(Type dataType)
        {
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType));

            _dataType = dataType;
            return this;
        }

        public ImageFormatBuilder WithNormalized(bool normalized)
        {
            _normalized = normalized;
            return this;
        }

        public ImageFormatBuilder WithChannelOrder(ImageChannelOrder channelOrder)
        {
            _channelOrder = channelOrder;
            return this;
        }

        public ImageFormatBuilder WithChannelType(ImageChannelType channelType)
        {
            _channelType = channelType;
            return this;
        }

        [SuppressMessage("ReSharper", "InvertIf")]
        public ImageFormat Build()
        {
            var channelOrder = ChannelOrderInfo.For(ChannelOrder);

            foreach (var channelType in GetChannelTypes())
            {
                if (channelOrder.SupportsChannelType(channelType) && channelType.SupportsChannelOrder(channelOrder))
                    return new ImageFormat(channelOrder, channelType);
            }

            ValidateChannelTypes();

            var channelTypes = string.Join(", ", GetChannelTypes().Select(info => info.ChannelType));

            var builder = new StringBuilder();

            builder.AppendLine("Cannot find compatible channel order and type for ");
            builder.Append("Channel order = ");
            builder.AppendLine(channelOrder.ChannelOrder.ToString());
            builder.Append("Channel types = ");
            builder.Append(channelTypes);

            throw new InvalidOperationException(builder.ToString());
        }

        [SuppressMessage("ReSharper", "InvertIf")]
        private IEnumerable<ChannelTypeInfo> GetChannelTypes()
        {
            if (_channelType.HasValue)
            {
                var info = ChannelTypeInfo.For(_channelType.Value);

                if (_normalized.HasValue && _normalized.Value != info.Normalized)
                    throw new InvalidOperationException(
                        $"{info.ChannelType} is not {(_normalized.Value ? "normalized" : "unnormalized")}");

                if (_dataType != null && _dataType != info.DataType)
                    throw new InvalidOperationException(
                        $"{info.ChannelType} does not use {_dataType.Name}");

                return new[] {info};
            }

            IEnumerable<ChannelTypeInfo> query = ChannelTypeInfo.All;

            if (_normalized.HasValue)
            {
                var normalized = _normalized.Value;
                query = query.Where(info => info.Normalized == normalized);
            }

            if (_dataType != null)
            {
                var dataType = _dataType;
                query = query.Where(info => info.DataType == dataType);
            }

            return query;
        }

        [SuppressMessage("ReSharper", "InvertIf")]
        private void ValidateChannelTypes()
        {
            if (GetChannelTypes().Any())
                return;

            var builder = new StringBuilder("Cannot find valid");

            if (_normalized.HasValue)
            {
                builder.Append(" ");
                builder.Append(_normalized.Value ? "normalized" : "unnormalized");
            }

            builder.Append(" channel type");

            if (_dataType != null)
            {
                builder.Append(" that uses ");
                builder.Append(_dataType.Name);
            }

            throw new InvalidOperationException(builder.ToString());
        }
    }
}
