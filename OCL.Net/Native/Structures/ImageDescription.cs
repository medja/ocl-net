using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OCL.Net.Native.Enums;

namespace OCL.Net.Native.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public struct ImageDescription
    {
        public MemObjectType Type => _imageType;
        public UIntPtr Width => _imageWidth;
        public UIntPtr Height => _imageHeight;
        public UIntPtr Depth => _imageDepth;
        public UIntPtr ArraySize => _imageArraySize;
        public UIntPtr RowPitch => _imageRowPitch;
        public UIntPtr SlicePitch => _imageSlicePitch;
        public uint NumMipLevels => _numMipLevels;
        public uint NumSamples => _numSamples;
        public BufferId Buffer => _buffer;

        private readonly MemObjectType _imageType;
        private readonly UIntPtr _imageWidth;
        private readonly UIntPtr _imageHeight;
        private readonly UIntPtr _imageDepth;
        private readonly UIntPtr _imageArraySize;
        private readonly UIntPtr _imageRowPitch;
        private readonly UIntPtr _imageSlicePitch;
        private readonly uint _numMipLevels;
        private readonly uint _numSamples;
        private readonly BufferId _buffer;

        public ImageDescription(MemObjectType type, UIntPtr width, UIntPtr height, UIntPtr depth,
            UIntPtr arraySize, UIntPtr rowPitch, UIntPtr slicePitch, uint numMipLevels, uint numSamples, BufferId buffer)
        {
            _imageType = type;
            _imageWidth = width;
            _imageHeight = height;
            _imageDepth = depth;
            _imageArraySize = arraySize;
            _imageRowPitch = rowPitch;
            _imageSlicePitch = slicePitch;
            _numMipLevels = numMipLevels;
            _numSamples = numSamples;
            _buffer = buffer;
        }

        #region Create1D

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1D(int width)
        {
            return Create1D((UIntPtr) width, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1D(ulong width)
        {
            return Create1D((UIntPtr) width, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1D(UIntPtr width)
        {
            return Create1D(width, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1D(int width, int rowPitch)
        {
            return Create1D((UIntPtr) width, (UIntPtr) rowPitch);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1D(ulong width, ulong rowPitch)
        {
            return Create1D((UIntPtr) width, (UIntPtr) rowPitch);
        }

        public static ImageDescription Create1D(UIntPtr width, UIntPtr rowPitch)
        {
            return new ImageDescription(
                MemObjectType.MemObjectImage1D, width, UIntPtr.Zero, UIntPtr.Zero,
                UIntPtr.Zero, rowPitch, UIntPtr.Zero, 0, 0, default);
        }

        #endregion

        #region Create1DBuffer

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1DBuffer(int width, BufferId bufferId)
        {
            return Create1DBuffer((UIntPtr) width, UIntPtr.Zero, bufferId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1DBuffer(ulong width, BufferId bufferId)
        {
            return Create1DBuffer((UIntPtr) width, UIntPtr.Zero, bufferId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1DBuffer(UIntPtr width, BufferId bufferId)
        {
            return Create1DBuffer(width, UIntPtr.Zero, bufferId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1DBuffer(int width, int rowPitch, BufferId bufferId)
        {
            return Create1DBuffer((UIntPtr) width, (UIntPtr) rowPitch, bufferId);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1DBuffer(ulong width, ulong rowPitch, BufferId bufferId)
        {
            return Create1DBuffer((UIntPtr) width, (UIntPtr) rowPitch, bufferId);
        }

        public static ImageDescription Create1DBuffer(UIntPtr width, UIntPtr rowPitch, BufferId bufferId)
        {
            return new ImageDescription(
                MemObjectType.MemObjectImage1DBuffer, width, UIntPtr.Zero, UIntPtr.Zero,
                UIntPtr.Zero, rowPitch, UIntPtr.Zero, 0, 0, bufferId);
        }

        #endregion

        #region Create1DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1DArray(int width, int arrayLength)
        {
            return Create1DArray((UIntPtr) width, UIntPtr.Zero, (UIntPtr) arrayLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1DArray(ulong width, ulong arrayLength)
        {
            return Create1DArray((UIntPtr) width, UIntPtr.Zero, (UIntPtr) arrayLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1DArray(UIntPtr width, UIntPtr arrayLength)
        {
            return Create1DArray(width, UIntPtr.Zero, arrayLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1DArray(int width, int rowPitch, int arrayLength)
        {
            return Create1DArray((UIntPtr) width, (UIntPtr) rowPitch, (UIntPtr) arrayLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create1DArray(ulong width, ulong rowPitch, ulong arrayLength)
        {
            return Create1DArray((UIntPtr) width, (UIntPtr) rowPitch, (UIntPtr) arrayLength);
        }

        public static ImageDescription Create1DArray(UIntPtr width, UIntPtr rowPitch, UIntPtr arrayLength)
        {
            return new ImageDescription(
                MemObjectType.MemObjectImage1DArray, width, UIntPtr.Zero, UIntPtr.Zero,
                arrayLength, rowPitch, UIntPtr.Zero, 0, 0, default);
        }

        #endregion

        #region Create2D

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2D(int width, int height)
        {
            return Create2D((UIntPtr) width, (UIntPtr) height, UIntPtr.Zero, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2D(ulong width, ulong height)
        {
            return Create2D((UIntPtr) width, (UIntPtr) height, UIntPtr.Zero, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2D(UIntPtr width, UIntPtr height)
        {
            return Create2D(width, height, UIntPtr.Zero, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2D(int width, int height, int rowPitch)
        {
            return Create2D((UIntPtr) width, (UIntPtr) height, (UIntPtr) rowPitch, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2D(ulong width, ulong height, ulong rowPitch)
        {
            return Create2D((UIntPtr) width, (UIntPtr) height, (UIntPtr) rowPitch, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2D(UIntPtr width, UIntPtr height, UIntPtr rowPitch)
        {
            return Create2D(width, height, rowPitch, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2D(int width, int height, int rowPitch, int slicePitch)
        {
            return Create2D((UIntPtr) width, (UIntPtr) height, (UIntPtr) rowPitch, (UIntPtr) slicePitch);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2D(ulong width, ulong height, ulong rowPitch, ulong slicePitch)
        {
            return Create2D((UIntPtr) width, (UIntPtr) height, (UIntPtr) rowPitch, (UIntPtr) slicePitch);
        }

        public static ImageDescription Create2D(UIntPtr width, UIntPtr height, UIntPtr rowPitch, UIntPtr slicePitch)
        {
            return new ImageDescription(MemObjectType.MemObjectImage2D, width, height, UIntPtr.Zero,
                UIntPtr.Zero, rowPitch, slicePitch, 0, 0, default);
        }

        #endregion

        #region Create2DArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2DArray(int width, int height, int arrayLength)
        {
            return Create2DArray((UIntPtr) width, (UIntPtr) height, UIntPtr.Zero, UIntPtr.Zero, (UIntPtr) arrayLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2DArray(ulong width, ulong height, ulong arrayLength)
        {
            return Create2DArray((UIntPtr) width, (UIntPtr) height, UIntPtr.Zero, UIntPtr.Zero, (UIntPtr) arrayLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2DArray(UIntPtr width, UIntPtr height, UIntPtr arrayLength)
        {
            return Create2DArray(width, height, UIntPtr.Zero, UIntPtr.Zero, arrayLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2DArray(int width, int height, int rowPitch, int arrayLength)
        {
            return Create2DArray((UIntPtr) width, (UIntPtr) height,
                (UIntPtr) rowPitch, UIntPtr.Zero, (UIntPtr) arrayLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2DArray(ulong width, ulong height, ulong rowPitch, ulong arrayLength)
        {
            return Create2DArray((UIntPtr) width, (UIntPtr) height,
                (UIntPtr) rowPitch, UIntPtr.Zero, (UIntPtr) arrayLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2DArray(UIntPtr width, UIntPtr height,
            UIntPtr rowPitch, UIntPtr arrayLength)
        {
            return Create2DArray(width, height, rowPitch, UIntPtr.Zero, arrayLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2DArray(int width, int height,
            int rowPitch, int slicePitch, int arrayLength)
        {
            return Create2DArray((UIntPtr) width, (UIntPtr) height,
                (UIntPtr) rowPitch, (UIntPtr) slicePitch, (UIntPtr) arrayLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create2DArray(ulong width, ulong height,
            ulong rowPitch, ulong slicePitch, ulong arrayLength)
        {
            return Create2DArray((UIntPtr) width, (UIntPtr) height,
                (UIntPtr) rowPitch, (UIntPtr) slicePitch, (UIntPtr) arrayLength);
        }

        public static ImageDescription Create2DArray(UIntPtr width, UIntPtr height,
            UIntPtr rowPitch, UIntPtr slicePitch, UIntPtr arrayLength)
        {
            return new ImageDescription(MemObjectType.MemObjectImage2DArray, width, height, UIntPtr.Zero,
                arrayLength, rowPitch, slicePitch, 0, 0, default);
        }

        #endregion

        #region Create3D

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create3D(int width, int height, int depth)
        {
            return Create3D((UIntPtr) width, (UIntPtr) height, (UIntPtr) depth, UIntPtr.Zero, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create3D(ulong width, ulong height, ulong depth)
        {
            return Create3D((UIntPtr) width, (UIntPtr) height, (UIntPtr) depth, UIntPtr.Zero, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create3D(UIntPtr width, UIntPtr height, UIntPtr depth)
        {
            return Create3D(width, height, depth, UIntPtr.Zero, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create3D(int width, int height, int depth, int rowPitch)
        {
            return Create3D((UIntPtr) width, (UIntPtr) height, (UIntPtr) depth, (UIntPtr) rowPitch, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create3D(ulong width, ulong height, ulong depth, ulong rowPitch)
        {
            return Create3D((UIntPtr) width, (UIntPtr) height, (UIntPtr) depth, (UIntPtr) rowPitch, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create3D(UIntPtr width, UIntPtr height, UIntPtr depth, UIntPtr rowPitch)
        {
            return Create3D(width, height, depth, rowPitch, UIntPtr.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create3D(int width, int height, int depth,
            int rowPitch, int slicePitch)
        {
            return Create3D((UIntPtr) width, (UIntPtr) height, (UIntPtr) depth,
                (UIntPtr) rowPitch, (UIntPtr) slicePitch);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImageDescription Create3D(ulong width, ulong height, ulong depth,
            ulong rowPitch, ulong slicePitch)
        {
            return Create3D((UIntPtr) width, (UIntPtr) height, (UIntPtr) depth,
                (UIntPtr) rowPitch, (UIntPtr) slicePitch);
        }

        public static ImageDescription Create3D(UIntPtr width, UIntPtr height, UIntPtr depth,
            UIntPtr rowPitch, UIntPtr slicePitch)
        {
            return new ImageDescription(MemObjectType.MemObjectImage3D, width, height, depth,
                UIntPtr.Zero, rowPitch, slicePitch, 0, 0, default);
        }

        #endregion
    }
}
