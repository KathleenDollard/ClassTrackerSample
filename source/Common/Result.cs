using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadGen.Common
{
    public class Result
    {
        protected Result(IEnumerable<Error> errors,
                IEnumerable<ValidationIssue> validationIssues)
        {
            Errors = errors ?? new List<Error>();
            ValidationIssues = validationIssues ?? new List<ValidationIssue>();
            IsSuccessful = Errors.Count() == 0 && ValidationIssues.Count() == 0;
        }

        protected Result(Error error) :
            this(new Error[] { error }, null)
        { }

        protected Result(ValidationIssue validationIssue) :
            this(null, new ValidationIssue[] { validationIssue })
        { }

        protected Result(IEnumerable<Error> errors) :
            this(errors, null)
        { }

        protected Result(IEnumerable<ValidationIssue> validationIssues) :
            this(null, validationIssues)
        { }

        public bool IsSuccessful { get; }
        public IEnumerable<Error> Errors { get; }
        public IEnumerable<ValidationIssue> ValidationIssues { get; }

        public static Result CreateSuccessResult()
            => new Result(null, null);

        public static Result CreateErrorResult(ErrorCode errorCode,
                Exception exception, string message)
            => new Result(new Error(errorCode, exception, message));

        public static Result CreateErrorResult(
                params (ErrorCode errorCode, Exception exception, string message)[] errorInfo)
            => new Result(errorInfo.Select(x => new Error(x.errorCode, x.exception, x.message)));

        public static Result CreateErrorResult(ValidationIssueId validationIssueId,
               params string[] fieldNames)
            => new Result(new ValidationIssue(validationIssueId, fieldNames));

        public static Result CreateErrorResult(
                params (ValidationIssueId validationIssueId, string[] fieldNames)[] validationInfo)
            => new Result(validationInfo.Select(x => new ValidationIssue(x.validationIssueId, x.fieldNames)));
    }

    public class DataResult<TData> : Result
    {
        protected DataResult(
                TData data,
                IEnumerable<Error> errors,
                IEnumerable<ValidationIssue> validationIssues)
            : base(errors, validationIssues)
        {
            Data = data;
        }

        protected DataResult(TData data) :
            this(data, (IEnumerable<Error>)null, null)
        { }

        protected DataResult(TData data,
            Error error) :
            this(data, new Error[] { error }, null)
        { }

        protected DataResult(TData data,
            ValidationIssue validationIssue) :
            this(data, null, new ValidationIssue[] { validationIssue })
        { }

        protected DataResult(TData data,
            params Error[] errors) :
            this(data, errors, null)
        { }

        protected DataResult(TData data,
            params ValidationIssue[] validationIssues) :
            this(data, null, validationIssues)
        { }

        public TData Data { get; }

        public static DataResult<TData> CreateSuccessResult(TData data)
            => new DataResult<TData>(data);

        public static DataResult<TData> CreateErrorResult(
                ErrorCode errorCode,
                Exception exception, string message)
            => new DataResult<TData>(default(TData), new Error(errorCode, exception, message));

        public static DataResult<TData> CreateErrorResult(
               params (ErrorCode errorCode, Exception exception, string message)[] errorInfo)
            => new DataResult<TData>(default(TData), errorInfo
                        .Select(x => new Error(x.errorCode, x.exception, x.message))
                        .ToArray());

        public static DataResult<TData> CreateErrorResult(
               ValidationIssueId validationIssueId,
               params string[] fieldNames)
            => new DataResult<TData>(default(TData), 
                        new ValidationIssue(validationIssueId, fieldNames));

        public static DataResult<TData> CreateErrorResult(
                params (ValidationIssueId validationIssueId, string[] fieldNames)[] validationInfo)
            => new DataResult<TData>(default(TData), validationInfo
                        .Select(x=>new ValidationIssue(x.validationIssueId, x.fieldNames))
                        .ToArray());
    }
}
