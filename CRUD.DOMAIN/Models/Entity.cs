using System.ComponentModel.DataAnnotations;

namespace CRUD.DOMAIN.Models;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }
}
