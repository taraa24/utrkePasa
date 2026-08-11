using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace UtrkePasa.Api.Dtos;


public class Result
{
    public bool IsFailed { get; set; }
    public string? ErrorCode { get; set; }
    public static Result Fail(string errorCode) => new() {IsFailed = true, ErrorCode = errorCode};
    public static Result Success() => new() {IsFailed = false};
}