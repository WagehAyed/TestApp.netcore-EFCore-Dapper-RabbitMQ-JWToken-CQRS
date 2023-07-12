using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.CommonWork
{
    public class BalanceUpdate
    {
        public string TypeOfInstruction { get; set; }
        public decimal Amount { get; set; }
    }
    public class NowBalanceClass
    {   public decimal Balance { get; set; }
    }

}
