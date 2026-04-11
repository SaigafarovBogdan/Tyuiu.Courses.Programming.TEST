namespace Tyuiu.Courses.Programming.Core.Shared
{
	public abstract class Entity<TId> : IEntity, IEquatable<Entity<TId>>
	where TId : notnull, IEquatable<TId>
	{
		public TId Id { get; protected set; }
		object IEntity.Id => Id;

		protected Entity()
		{
			Id = default!;
		}

		protected Entity(TId id)
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
		}

		public override bool Equals(object? obj)
		{
			return Equals(obj as Entity<TId>);
		}

		public bool Equals(Entity<TId>? other)
		{
			if (other is null)
				return false;
			if (ReferenceEquals(this, other))
				return true;
			if (GetType() != other.GetType())
				return false;

			return Id.Equals(other.Id);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GetType(), Id);
		}

		public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
		{
			if (left is null)
				return right is null;
			return left.Equals(right);
		}

		public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
		{
			return !(left == right);
		}
	}

	public abstract class Entity : Entity<int>
	{
		protected Entity(int id) : base(id) {}

		protected Entity() : base(default!) {}
	}
}
