using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using OCL.Net.Native.Structures;

namespace OCL.Net
{
    public sealed partial class KernelArgument
    {
        private static readonly Dictionary<string, Type> BaseTypes;
        private static readonly Regex TypeNameRegex;

        static KernelArgument()
        {
            BaseTypes = new Dictionary<string, Type>
            {
                {"bool", typeof(bool)},
                {"_bool", typeof(bool)},
                {"char", typeof(sbyte)},
                {"unsigned char", typeof(byte)},
                {"uchar", typeof(byte)},
                {"short", typeof(short)},
                {"unsigned short", typeof(ushort)},
                {"ushort", typeof(ushort)},
                {"int", typeof(int)},
                {"unsigned int", typeof(uint)},
                {"uint", typeof(uint)},
                {"long", typeof(long)},
                {"unsigned long", typeof(ulong)},
                {"ulong", typeof(ulong)},
                {"float", typeof(float)},
                {"double", typeof(double)},
                {"size_t", typeof(UIntPtr)},
                {"intptr_t", typeof(IntPtr)},
                {"uintptr_t", typeof(UIntPtr)},
                {"ptrdiff_t", typeof(IntPtr)},
                {"image1d_t", typeof(Image1D)},
                {"image1d_buffer_t", typeof(Image1D)},
                {"image1d_array_t", typeof(Image1DArray)},
                {"image2d_t", typeof(Image2D)},
                {"image2d_array_t", typeof(Image2DArray)},
                {"image3d_t", typeof(Image3D)},
                {"sampler_t", typeof(Sampler)},
                {"event_t", typeof(Event)}
            };

            var pattern = "^(" + string.Join("|", BaseTypes.Keys.Select(Regex.Escape)) + ")(2|3|4|8|16)?(\\*)?$";
            TypeNameRegex = new Regex(pattern, RegexOptions.IgnoreCase);
        }

        public Type Type => _type.Value;

        private readonly Lazy<Type> _type;

        private Type FindType()
        {
            var match = TypeNameRegex.Match(TypeName);

            if (!match.Success)
                return null;

            var baseType = BaseTypes[match.Groups[1].Value.ToLower()];

            if (match.Groups[2].Success)
                return null;

            if (match.Groups[3].Success)
                return typeof(Buffer<>).MakeGenericType(GetNativeType(baseType));

            return baseType;
        }

        [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
        private Type GetNativeType(Type managedType)
        {
            if (managedType == typeof(Sampler))
                return typeof(SamplerId);

            if (managedType == typeof(Event))
                return typeof(EventId);

            return managedType;
        }
    }
}
