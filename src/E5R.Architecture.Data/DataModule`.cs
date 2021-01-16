// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using E5R.Architecture.Data.Abstractions;

namespace E5R.Architecture.Data
{
    /// <summary>
    /// Data module with single storage unit
    /// </summary>
    /// <typeparam name="TStorage1">Type of storage 1</typeparam>
    public class DataModule<TStorage1> : IDataMouleSignature
        where TStorage1 : IStorageSignature
    {
        protected TStorage1 Storage1 { get; set; }
    }

    /// <summary>
    /// Data module with two storage units
    /// </summary>
    /// <typeparam name="TStorage1">Type of storage 1</typeparam>
    /// <typeparam name="TStorage2">Type of storage 2</typeparam>
    public class DataModule<TStorage1, TStorage2> : IDataMouleSignature
        where TStorage1 : IStorageSignature
        where TStorage2 : IStorageSignature
    {
        protected TStorage1 Storage1 { get; set; }
        protected TStorage2 Storage2 { get; set; }
    }

    /// <summary>
    /// Data module with three storage units
    /// </summary>
    /// <typeparam name="TStorage1">Type of storage 1</typeparam>
    /// <typeparam name="TStorage2">Type of storage 2</typeparam>
    /// <typeparam name="TStorage3">Type of storage 3</typeparam>
    public class DataModule<TStorage1, TStorage2, TStorage3> : IDataMouleSignature
        where TStorage1 : IStorageSignature
        where TStorage2 : IStorageSignature
        where TStorage3 : IStorageSignature
    {
        protected TStorage1 Storage1 { get; set; }
        protected TStorage2 Storage2 { get; set; }
        protected TStorage3 Storage3 { get; set; }
    }

    /// <summary>
    /// Data module with four storage units
    /// </summary>
    /// <typeparam name="TStorage1">Type of storage 1</typeparam>
    /// <typeparam name="TStorage2">Type of storage 2</typeparam>
    /// <typeparam name="TStorage3">Type of storage 3</typeparam>
    /// <typeparam name="TStorage4">Type of storage 4</typeparam>
    public class DataModule<TStorage1, TStorage2, TStorage3, TStorage4> : IDataMouleSignature
        where TStorage1 : IStorageSignature
        where TStorage2 : IStorageSignature
        where TStorage3 : IStorageSignature
        where TStorage4 : IStorageSignature
    {
        protected TStorage1 Storage1 { get; set; }
        protected TStorage2 Storage2 { get; set; }
        protected TStorage3 Storage3 { get; set; }
        protected TStorage4 Storage4 { get; set; }
    }

    /// <summary>
    /// Data module with five storage units
    /// </summary>
    /// <typeparam name="TStorage1">Type of storage 1</typeparam>
    /// <typeparam name="TStorage2">Type of storage 2</typeparam>
    /// <typeparam name="TStorage3">Type of storage 3</typeparam>
    /// <typeparam name="TStorage4">Type of storage 4</typeparam>
    /// <typeparam name="TStorage5">Type of storage 5</typeparam>
    public class
        DataModule<TStorage1, TStorage2, TStorage3, TStorage4, TStorage5> : IDataMouleSignature
        where TStorage1 : IStorageSignature
        where TStorage2 : IStorageSignature
        where TStorage3 : IStorageSignature
        where TStorage4 : IStorageSignature
        where TStorage5 : IStorageSignature
    {
        protected TStorage1 Storage1 { get; set; }
        protected TStorage2 Storage2 { get; set; }
        protected TStorage3 Storage3 { get; set; }
        protected TStorage4 Storage4 { get; set; }
        protected TStorage5 Storage5 { get; set; }
    }

    /// <summary>
    /// Data module with six storage units
    /// </summary>
    /// <typeparam name="TStorage1">Type of storage 1</typeparam>
    /// <typeparam name="TStorage2">Type of storage 2</typeparam>
    /// <typeparam name="TStorage3">Type of storage 3</typeparam>
    /// <typeparam name="TStorage4">Type of storage 4</typeparam>
    /// <typeparam name="TStorage5">Type of storage 5</typeparam>
    /// <typeparam name="TStorage6">Type of storage 6</typeparam>
    public class
        DataModule<TStorage1, TStorage2, TStorage3, TStorage4, TStorage5,
            TStorage6> : IDataMouleSignature
        where TStorage1 : IStorageSignature
        where TStorage2 : IStorageSignature
        where TStorage3 : IStorageSignature
        where TStorage4 : IStorageSignature
        where TStorage5 : IStorageSignature
        where TStorage6 : IStorageSignature
    {
        protected TStorage1 Storage1 { get; set; }
        protected TStorage2 Storage2 { get; set; }
        protected TStorage3 Storage3 { get; set; }
        protected TStorage4 Storage4 { get; set; }
        protected TStorage5 Storage5 { get; set; }
        protected TStorage6 Storage6 { get; set; }
    }

    /// <summary>
    /// Data module with seven storage units
    /// </summary>
    /// <typeparam name="TStorage1">Type of storage 1</typeparam>
    /// <typeparam name="TStorage2">Type of storage 2</typeparam>
    /// <typeparam name="TStorage3">Type of storage 3</typeparam>
    /// <typeparam name="TStorage4">Type of storage 4</typeparam>
    /// <typeparam name="TStorage5">Type of storage 5</typeparam>
    /// <typeparam name="TStorage6">Type of storage 6</typeparam>
    /// <typeparam name="TStorage7">Type of storage 7</typeparam>
    public class DataModule<TStorage1, TStorage2, TStorage3, TStorage4, TStorage5, TStorage6,
        TStorage7> : IDataMouleSignature
        where TStorage1 : IStorageSignature
        where TStorage2 : IStorageSignature
        where TStorage3 : IStorageSignature
        where TStorage4 : IStorageSignature
        where TStorage5 : IStorageSignature
        where TStorage6 : IStorageSignature
        where TStorage7 : IStorageSignature
    {
        protected TStorage1 Storage1 { get; set; }
        protected TStorage2 Storage2 { get; set; }
        protected TStorage3 Storage3 { get; set; }
        protected TStorage4 Storage4 { get; set; }
        protected TStorage5 Storage5 { get; set; }
        protected TStorage6 Storage6 { get; set; }
        protected TStorage7 Storage7 { get; set; }
    }

    /// <summary>
    /// Data module with eight storage units
    /// </summary>
    /// <typeparam name="TStorage1">Type of storage 1</typeparam>
    /// <typeparam name="TStorage2">Type of storage 2</typeparam>
    /// <typeparam name="TStorage3">Type of storage 3</typeparam>
    /// <typeparam name="TStorage4">Type of storage 4</typeparam>
    /// <typeparam name="TStorage5">Type of storage 5</typeparam>
    /// <typeparam name="TStorage6">Type of storage 6</typeparam>
    /// <typeparam name="TStorage7">Type of storage 7</typeparam>
    /// <typeparam name="TStorage8">Type of storage 8</typeparam>
    public class DataModule<TStorage1, TStorage2, TStorage3, TStorage4, TStorage5, TStorage6,
        TStorage7, TStorage8> : IDataMouleSignature
        where TStorage1 : IStorageSignature
        where TStorage2 : IStorageSignature
        where TStorage3 : IStorageSignature
        where TStorage4 : IStorageSignature
        where TStorage5 : IStorageSignature
        where TStorage6 : IStorageSignature
        where TStorage7 : IStorageSignature
        where TStorage8 : IStorageSignature
    {
        protected TStorage1 Storage1 { get; set; }
        protected TStorage2 Storage2 { get; set; }
        protected TStorage3 Storage3 { get; set; }
        protected TStorage4 Storage4 { get; set; }
        protected TStorage5 Storage5 { get; set; }
        protected TStorage6 Storage6 { get; set; }
        protected TStorage7 Storage7 { get; set; }
        protected TStorage8 Storage8 { get; set; }
    }

    /// <summary>
    /// Data module with nine storage units
    /// </summary>
    /// <typeparam name="TStorage1">Type of storage 1</typeparam>
    /// <typeparam name="TStorage2">Type of storage 2</typeparam>
    /// <typeparam name="TStorage3">Type of storage 3</typeparam>
    /// <typeparam name="TStorage4">Type of storage 4</typeparam>
    /// <typeparam name="TStorage5">Type of storage 5</typeparam>
    /// <typeparam name="TStorage6">Type of storage 6</typeparam>
    /// <typeparam name="TStorage7">Type of storage 7</typeparam>
    /// <typeparam name="TStorage8">Type of storage 8</typeparam>
    /// <typeparam name="TStorage9">Type of storage 9</typeparam>
    public class DataModule<TStorage1, TStorage2, TStorage3, TStorage4, TStorage5, TStorage6,
        TStorage7, TStorage8, TStorage9> : IDataMouleSignature
        where TStorage1 : IStorageSignature
        where TStorage2 : IStorageSignature
        where TStorage3 : IStorageSignature
        where TStorage4 : IStorageSignature
        where TStorage5 : IStorageSignature
        where TStorage6 : IStorageSignature
        where TStorage7 : IStorageSignature
        where TStorage8 : IStorageSignature
        where TStorage9 : IStorageSignature
    {
        protected TStorage1 Storage1 { get; set; }
        protected TStorage2 Storage2 { get; set; }
        protected TStorage3 Storage3 { get; set; }
        protected TStorage4 Storage4 { get; set; }
        protected TStorage5 Storage5 { get; set; }
        protected TStorage6 Storage6 { get; set; }
        protected TStorage7 Storage7 { get; set; }
        protected TStorage8 Storage8 { get; set; }
        protected TStorage9 Storage9 { get; set; }
    }

    /// <summary>
    /// Data module with ten storage units
    /// </summary>
    /// <typeparam name="TStorage1">Type of storage 1</typeparam>
    /// <typeparam name="TStorage2">Type of storage 2</typeparam>
    /// <typeparam name="TStorage3">Type of storage 3</typeparam>
    /// <typeparam name="TStorage4">Type of storage 4</typeparam>
    /// <typeparam name="TStorage5">Type of storage 5</typeparam>
    /// <typeparam name="TStorage6">Type of storage 6</typeparam>
    /// <typeparam name="TStorage7">Type of storage 7</typeparam>
    /// <typeparam name="TStorage8">Type of storage 8</typeparam>
    /// <typeparam name="TStorage9">Type of storage 9</typeparam>
    /// <typeparam name="TStorage10">Type of storage 10</typeparam>
    public class DataModule<TStorage1, TStorage2, TStorage3, TStorage4, TStorage5, TStorage6,
        TStorage7, TStorage8, TStorage9, TStorage10> : IDataMouleSignature
        where TStorage1 : IStorageSignature
        where TStorage2 : IStorageSignature
        where TStorage3 : IStorageSignature
        where TStorage4 : IStorageSignature
        where TStorage5 : IStorageSignature
        where TStorage6 : IStorageSignature
        where TStorage7 : IStorageSignature
        where TStorage8 : IStorageSignature
        where TStorage9 : IStorageSignature
        where TStorage10 : IStorageSignature
    {
        protected TStorage1 Storage1 { get; set; }
        protected TStorage2 Storage2 { get; set; }
        protected TStorage3 Storage3 { get; set; }
        protected TStorage4 Storage4 { get; set; }
        protected TStorage5 Storage5 { get; set; }
        protected TStorage6 Storage6 { get; set; }
        protected TStorage7 Storage7 { get; set; }
        protected TStorage8 Storage8 { get; set; }
        protected TStorage9 Storage9 { get; set; }
        protected TStorage10 Storage10 { get; set; }
    }
}
