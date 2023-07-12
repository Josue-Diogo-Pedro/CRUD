using System.ComponentModel.DataAnnotations;

namespace CRUD.DOMAIN.Models;

public class Produto : Entity
{
    public Guid CategoriaId { get; set; }
    public Categoria Categoria { get; set; }

    [Required(ErrorMessage = "O campo Título é obrigatório!")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres")]
    [Display(Name = "Título")]
    public string Titulo { get; set; }

    [StringLength(80, MinimumLength = 3, ErrorMessage = "O campo Descrição deve ter no mínimo 3 caracteres!")]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }
    public string Imagem { get; set; }

}
