using System;
using System.Collections;
using JetBrains.Annotations;

public sealed class BitArray2D : ICloneable
{
	public int Width { get; }
	public int Height { get; }

	public bool this[int x, int y]
	{
		get => _bitArray[CoordinatesToIndex(x, y)];
		set => _bitArray[CoordinatesToIndex(x, y)] = value;
	}

	private int CoordinatesToIndex(int x, int y) => x + y * Width;

	public BitArray2D(int width, int height)
	{
		if (width < 0) throw new ArgumentOutOfRangeException(nameof(width));
		if (height < 0) throw new ArgumentOutOfRangeException(nameof(height));

		Width = width;
		Height = height;
		_bitArray = new BitArray(width * height);
	}

	object ICloneable.Clone() => Clone();

	public BitArray2D Clone() => new BitArray2D(Width, Height, _bitArray);

	private BitArray2D(int width, int height, [NotNull] BitArray bitArray)
	{
		if (width < 0) throw new ArgumentOutOfRangeException(nameof(width));
		if (height < 0) throw new ArgumentOutOfRangeException(nameof(height));
		if (bitArray == null) throw new ArgumentNullException(nameof(bitArray));
		if (width * height != bitArray.Count)
			throw new ArgumentException("Dimensions do not match to the size of BitArray.");

		Width = width;
		Height = height;
		_bitArray = (BitArray) bitArray.Clone();
	}

	private readonly BitArray _bitArray;
}