﻿using System.Runtime.CompilerServices;
using Arch.Core;

namespace Arch.Relationships;

/// <summary>
///     Interface implemented by <see cref="Relationship{T}"/>.
/// </summary>
internal interface IBuffer
{
    /// <summary>
    ///     Comparer used to sort <see cref="Entity"/> relationships.
    /// </summary>
    internal static readonly Comparer<Entity> Comparer = Comparer<Entity>.Create((a, b) => a.Id.CompareTo(b.Id));

    /// <summary>
    ///     The amount of relationships currently in the buffer.
    /// </summary>
    int Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

    /// <summary>
    ///     Removes the buffer as a component from the given world and entity.
    /// </summary>
    /// <param name="world"></param>
    /// <param name="source"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Destroy(World world, Entity source);

    /// <summary>
    ///     Removes the relationship targeting <see cref="target"/> from this buffer.
    /// </summary>
    /// <param name="target">The <see cref="Entity"/> in the relationship to remove.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Remove(Entity target);
}

/// <summary>
///     A buffer storing relationships of <see cref="Entity"/> and <see cref="T"/>.
/// </summary>
/// <typeparam name="T">The type of the second relationship element.</typeparam>
internal class Relationship<T> : IBuffer
{
    /// <summary>
    ///     Its relations. 
    /// </summary>
    internal readonly SortedList<Entity, T> Elements;

    /// <summary>
    ///     Initializes a new instance of an <see cref="Relationship{T}"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Relationship()
    {
        Elements = new SortedList<Entity, T>(IBuffer.Comparer);
    }

    /// <inheritdoc/>
    int IBuffer.Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Elements.Count;
    }

    /// <inheritdoc cref="IBuffer.Count"/>
    internal int Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ((IBuffer) this).Count;
    }

    /// <summary>
    ///     Adds a relationship to this buffer.
    /// </summary>
    /// <param name="relationship">The instance of the relationship.</param>
    /// <param name="target">The target of the relationship.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Add(in T relationship, Entity target)
    {
        Elements.Add(target, relationship);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IBuffer.Destroy(World world, Entity source)
    {
        world.Remove<Relationship<T>>(source);
    }

    /// <inheritdoc cref="IBuffer.Destroy(World, Entity)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Destroy(World world, Entity source)
    {
        ((IBuffer) this).Destroy(world, source);
    }

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IBuffer.Remove(Entity target)
    {
        Elements.Remove(target);
    }

    /// <inheritdoc cref="IBuffer.Remove(Entity)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Remove(Entity target)
    {
        ((IBuffer) this).Remove(target);
    }
};