using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StateMachine.Service.StateInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine.Service.StateMaps
{
    public class OrderStateMap : SagaClassMap<OrderStateInstance>
    {
        //state e ait validasyonlar
        protected override void Configure(EntityTypeBuilder<OrderStateInstance> entity, ModelBuilder model)
        {
            entity.Property(x => x.BuyerId).IsRequired();
            entity.Property(x => x.OrderId).IsRequired();
            entity.Property(x => x.TotalPrice).HasDefaultValue(0);
        }
    }
}
