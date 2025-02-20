global using BuildingBlocks.CQRS;
global using BuildingBlocks.Pagination;

global using MediatR;
global using Microsoft.Extensions.Logging;
global using Microsoft.EntityFrameworkCore;

global using FluentValidation;
global using BuildingBlocks.Messaging.MassTransit;


global using Ordering.Application.Dtos;
global using Ordering.Application.Data;
global using Ordering.Application.Extensions;
global using Ordering.Domain.Models;
global using Ordering.Application.Exceptions;
global using Ordering.Domain.Events;
global using Ordering.Domain.Value_Objects;