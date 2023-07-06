using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Commands
{
    public abstract class Command<TResult>:IRequest<TResult>,ICommand
    {
        public Guid CommandId { get; }=Guid.NewGuid();
    }
}
