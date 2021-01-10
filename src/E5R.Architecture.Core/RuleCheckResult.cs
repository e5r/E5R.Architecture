// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;

namespace E5R.Architecture.Core
{
    public class RuleCheckResult
    {
        static RuleCheckResult _defaultSuccessResult;
        static RuleCheckResult _defaultFailResult;

        public RuleCheckResult(bool success, IDictionary<string, string> unconformities = null)
        {
            IsSuccess = success;
            unconformities = unconformities ?? new Dictionary<string, string>();
        }

        public bool IsSuccess { get; }

        public IDictionary<string, string> Unconformities { get; }

        public static RuleCheckResult Success
        {
            get
            {
                if (_defaultSuccessResult == null)
                {
                    _defaultSuccessResult = new RuleCheckResult(true);
                }

                return _defaultSuccessResult;
            }
        }

        public static RuleCheckResult Fail
        {
            get
            {
                if (_defaultFailResult == null)
                {
                    _defaultFailResult = new RuleCheckResult(false);
                }

                return _defaultFailResult;
            }
        }
    }
}
