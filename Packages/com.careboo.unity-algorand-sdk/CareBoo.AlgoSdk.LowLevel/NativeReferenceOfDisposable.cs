using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk.LowLevel
{
    public struct NativeReferenceOfDisposable<T>
        : INativeDisposable
        where T : unmanaged, INativeDisposable
    {
        NativeReference<T> reference;

        public NativeReferenceOfDisposable(T value, Allocator allocator)
            : this(new NativeReference<T>(value, allocator)) { }

        public NativeReferenceOfDisposable(NativeReference<T> reference)
        {
            this.reference = reference;
        }

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return reference.IsCreated
                ? reference.Dispose(reference.Value.Dispose(inputDeps))
                : inputDeps;
        }

        public bool IsCreated => reference.IsCreated;

        public T Value => reference.Value;

        public NativeReference<T>.ReadOnly AsReadOnly() => reference.AsReadOnly();

        public void Dispose()
        {
            if (reference.IsCreated)
            {
                reference.Value.Dispose();
                reference.Dispose();
            }
        }

        public static implicit operator NativeReferenceOfDisposable<T>(NativeReference<T> reference)
        {
            return new NativeReferenceOfDisposable<T>(reference);
        }
    }
}
