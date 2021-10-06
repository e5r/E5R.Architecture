// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Threading;
using System.Threading.Tasks;

namespace E5R.Architecture.Core.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Waits for the <see cref="Task"/> to complete execution.
        /// </summary>
        /// <typeparam name="TResult">The task result type</typeparam>
        /// <param name="task">The task</param>
        /// <returns>The task result value</returns>
        public static TResult WaitResult<TResult>(this Task<TResult> task)
        {
            Checker.NotNullArgument(task, nameof(task));

            task.Wait();

            return task.Result;
        }

        /// <summary>
        /// Waits for the <see cref="Task"/> to complete execution. The wait terminates
        /// if a cancellation token is canceled before the task completes.
        /// </summary>
        /// <typeparam name="TResult">The task result type</typeparam>
        /// <param name="task">The task</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete</param>
        /// <returns>The task result value</returns>
        public static TResult WaitResult<TResult>(this Task<TResult> task, CancellationToken cancellationToken)
        {
            Checker.NotNullArgument(task, nameof(task));
            Checker.NotNullArgument(cancellationToken, nameof(cancellationToken));

            task.Wait(cancellationToken);

            return task.Result;
        }

        /// <summary>
        ///  Waits for the <see cref="Task"/> to complete execution within a specified
        ///  number of milliseconds.
        /// </summary>
        /// <typeparam name="TResult">The task result type</typeparam>
        /// <param name="task">The task</param>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or <see cref="Timeout.Infinite" /> (-1)
        /// to wait indefinitely.</param>
        /// <returns>
        /// The task result value and, true if the <see cref="Task"/> completed execution
        /// within the allotted time; otherwise, false.
        /// </returns>
        public static (TResult, bool) WaitResult<TResult>(this Task<TResult> task, int millisecondsTimeout)
        {
            Checker.NotNullArgument(task, nameof(task));

            bool completed = task.Wait(millisecondsTimeout);

            return (task.Result, completed);
        }

        /// <summary>
        /// Waits for the <see cref="Task"/> to complete execution within a specified time interval.
        /// </summary>
        /// <typeparam name="TResult">The task result type</typeparam>
        /// <param name="task">The task</param>
        /// <param name="timeout">
        /// A <see cref="TimeSpan"/> that represents the number of milliseconds to wait, or a 
        /// <see cref="TimeSpan"/> that represents -1 milliseconds to wait indefinitely.
        /// </param>
        /// <returns>
        /// The task result value and, true if the <see cref="Task"/> completed execution
        /// within the allotted time; otherwise, false.
        /// </returns>
        public static (TResult, bool) WaitResult<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            Checker.NotNullArgument(task, nameof(task));

            bool completed = task.Wait(timeout);

            return (task.Result, completed);
        }

        /// <summary>
        /// Waits for the <see cref="Task"/> to complete execution. The wait terminates if a timeout
        /// interval elapses or a cancellation token is canceled before the task completes.
        /// </summary>
        /// <typeparam name="TResult">The task result type</typeparam>
        /// <param name="task">The task</param>
        /// <param name="millisecondsTimeout">
        /// The number of milliseconds to wait, or <see cref="Timeout.Infinite" /> (-1) to wait indefinitely.
        /// </param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>
        /// The task result value and, true if the <see cref="Task"/> completed execution within the allotted
        //  time; otherwise, false.
        /// </returns>
        public static (TResult, bool) WaitResult<TResult>(this Task<TResult> task, int millisecondsTimeout, CancellationToken cancellationToken)
        {
            Checker.NotNullArgument(task, nameof(task));
            Checker.NotNullArgument(cancellationToken, nameof(cancellationToken));

            bool completed = task.Wait(millisecondsTimeout, cancellationToken);

            return (task.Result, completed);
        }
    }
}
