using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine.Service.StateInstance
{
    public class OrderStateInstance : SagaStateMachineInstance
    {
        //state machinedeki siparis instanceına karşılık gelen sınıf
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
