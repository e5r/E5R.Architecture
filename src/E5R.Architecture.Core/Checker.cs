// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace E5R.Architecture.Core
{
    public static class Checker
    {
        private static string ExtractArgumentName(Expression<Func<object>> expression)
        {
            string argName = null;

            if (expression?.Body.NodeType == ExpressionType.MemberAccess)
            {
                var nameMembers = new List<string>();
                var memberExpression = expression.Body as MemberExpression;

                while (memberExpression != null)
                {
                    nameMembers.Add(memberExpression.Member.Name);

                    if (memberExpression.Expression.NodeType != ExpressionType.MemberAccess)
                        break;

                    memberExpression = memberExpression.Expression as MemberExpression;
                }

                nameMembers.Reverse();

                argName = string.Join(".", nameMembers);
            }
            else if (expression != null)
            {
                // TODO: Implementar i18n/l10n
                throw new InvalidExpressionException("Only MemberExpression are allowed");
            }

            return argName;
        }

        public static void NotNullArgument(object argObj, Expression<Func<object>> expression)
        {
            if (argObj == null)
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(ExtractArgumentName(expression));
            }
        }

        public static void NotNullArgument(object argObj, string argName)
        {
            if (argObj == null)
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(argName);
            }
        }

        public static void NotNullObject(object @object, Expression<Func<object>> expression)
        {
            if (@object == null)
            {
                // TODO: Implementar i18n/l10n
                throw new NullReferenceException(
                    $"Object {ExtractArgumentName(expression)} can not be null");
            }
        }

        public static void NotNullObject(object @object, string objName)
        {
            if (@object == null)
            {
                // TODO: Implementar i18n/l10n
                throw new NullReferenceException($"Object {objName} can not be null");
            }
        }

        public static void NotNullOrEmptyArgument<T>(T[] argArray,
            Expression<Func<object>> expression)
        {
            if (argArray == null || argArray.Length < 1)
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(ExtractArgumentName(expression));
            }
        }

        public static void NotNullOrEmptyArgument<T>(T[] argArray, string argName)
        {
            if (argArray == null || argArray.Length < 1)
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(argName);
            }
        }

        public static void NotEmptyArgument(string argStr, Expression<Func<object>> expression)
        {
            if (string.IsNullOrEmpty(argStr))
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(ExtractArgumentName(expression));
            }
        }

        public static void NotEmptyArgument(string argStr, string argName)
        {
            if (string.IsNullOrEmpty(argStr))
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(argName);
            }
        }

        public static void NotEmptyOrWhiteArgument(string argStr,
            Expression<Func<object>> expression)
        {
            if (string.IsNullOrWhiteSpace(argStr))
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(ExtractArgumentName(expression));
            }
        }

        public static void NotEmptyOrWhiteArgument(string argStr, string argName)
        {
            if (string.IsNullOrWhiteSpace(argStr))
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentNullException(argName);
            }
        }
    }
}
