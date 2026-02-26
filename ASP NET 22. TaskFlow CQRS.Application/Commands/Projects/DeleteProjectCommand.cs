using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Commands.Projects;

public record DeleteProjectCommand(int Id) : IRequest<bool>;