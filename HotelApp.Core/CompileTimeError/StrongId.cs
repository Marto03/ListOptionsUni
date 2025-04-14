namespace HotelApp.Core.CompileTimeError
{
    public readonly struct StrongId<T> : IEquatable<StrongId<T>>
    {
        public int Value { get; }

        public StrongId(int value)
        {
            Value = value;
        }

        public bool Equals(StrongId<T> other) => Value == other.Value;

        public override bool Equals(object obj) =>
            obj is StrongId<T> other && Equals(other);

        public override int GetHashCode() => Value;

        public override string ToString() => Value.ToString();

        public static bool operator ==(StrongId<T> left, StrongId<T> right) => left.Equals(right);
        public static bool operator !=(StrongId<T> left, StrongId<T> right) => !(left == right);
    }
    // трябва да се направят ID-тата така -> StrongId<FacilityIdType> Id в Моделите , за да хвърли Compile-time грешка при сравняване на 2 различни типа!
    public class FacilityIdType { }
    public class RoomTypeIdType { }
    public class PaymentMethodIdType { }

}
