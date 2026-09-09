using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.BusinessLogic.DTO
{
    public class TransferRequestDto
    {
        public int FromAccountId {  get; set; }
        public string? ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }

    }
}
