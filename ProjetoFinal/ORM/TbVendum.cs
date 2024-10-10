using System;
using System.Collections.Generic;

namespace ProjetoFinal.ORM;

public partial class TbVendum
{
    public int Id { get; set; }

    public int Valor { get; set; }

    public string NotaF { get; set; } = null!;

    public int FkProduto { get; set; }

    public int FkCliente { get; set; }

    public virtual TbCliente FkClienteNavigation { get; set; } = null!;

    public virtual TbProduto FkProdutoNavigation { get; set; } = null!;
}
