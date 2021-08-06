// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.Threading.Tasks;

namespace E5R.Architecture.Infrastructure.AspNetCore.Workers
{
    public class DynamicDelay
    {
        private int Min { get; }
        private int Max { get; }
        private int Increment { get; }
        
        public DynamicDelay(int min, int max, int increment)
        {
            if (min < 1)
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentOutOfRangeException(
                    "Dynamic increment requires a minimum value between 1 and int.MaxValue");
            }

            if (max < 1)
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentOutOfRangeException(
                    "Dynamic increment requires a maximum value between 1 and int.MaxValue");
            }

            if (increment < 1)
            {
                // TODO: Implementar i18n/l10n
                throw new ArgumentOutOfRangeException(
                    "Dynamic increment requires an increment value between 1 and int.MaxValue");
            }

            Min = min;
            Max = max;
            Increment = increment;
            
            Reset();
        }

        public int CurrentInterval { get; private set; }

        public void Reset()
        {
            CurrentInterval = Min;
        }

        public async Task Wait()
        {
            await Task.Delay(TimeSpan.FromSeconds(CurrentInterval));

            CurrentInterval += Increment;

            if (CurrentInterval > Max)
            {
                CurrentInterval = Max;
            }
        }

        public async Task WaitMinimum()
        {
            await Task.Delay(TimeSpan.FromSeconds(Min));
        }
    }
}
