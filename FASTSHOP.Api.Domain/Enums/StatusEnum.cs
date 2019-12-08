using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FASTSHOP.Api.Domain.Enums
{
    public enum StatusEnum
    {
        [Description("Ativo")]
        Active = 1,
        [Description("Desativado")]
        Desactive = 2,
        [Description("Excluído")]
        Excluded = 3,
        [Description("Concluido")]
        Completed = 4,
        [Description("Em Estoque")]
        InStock = 7,
        [Description("Fora do Estoque")]
        OutStock = 8,
        [Description("Pendente")]
        Pending = 5,
        [Description("Em Processamento")]
        Processing = 6,
        [Description("Cancelado")]
        Canceled = 9,
        [Description("Entregue")]
        Deliveryed = 10,
    }
}

