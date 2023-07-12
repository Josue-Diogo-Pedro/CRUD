using System.ComponentModel.DataAnnotations;

namespace CRUD.DOMAIN.Models;

public class Categoria : Entity
{
    [StringLength(80, MinimumLength = 3, ErrorMessage = "A descrição deve ter no mínimo 3 caracteres!")]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }
    public List<Produto> Produtos { get; set; }
}
