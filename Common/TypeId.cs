namespace Common
{
    public readonly struct TypedId<T>
    {
        private readonly int value;

        public TypedId(int value) => this.value = value;

        public override string ToString() => value.ToString();

        public static bool operator ==(TypedId<T> left, TypedId<T> right) => left.value == right.value;
        public static bool operator !=(TypedId<T> left, TypedId<T> right) => left.value != right.value;

        public override bool Equals(object? obj) => obj is TypedId<T> other && value == other.value;
        public override int GetHashCode() => value.GetHashCode();

        public int ToInt() => value;
    }
    public readonly struct UserType
    {
        public static readonly UserType Admin = new UserType(1);
        public static readonly UserType User = new UserType(2);

        private readonly int value;

        private UserType(int value) => this.value = value;

        public static implicit operator int(UserType type) => type.value;
    }

    public readonly struct FacilityType
    {
        public static readonly FacilityType Gym = new FacilityType(1);
        public static readonly FacilityType Pool = new FacilityType(2);

        private readonly int value;

        private FacilityType(int value) => this.value = value;

        public static implicit operator int(FacilityType type) => type.value;
    }
    public static class IdExtensions
    {
        public static TypedId<UserTypeEnum> ToUserTypeId(this int id) => new TypedId<UserTypeEnum>(id);
        public static TypedId<FacilityTypeEnum> ToFacilityTypeId(this int id) => new TypedId<FacilityTypeEnum>(id);
    }

}
