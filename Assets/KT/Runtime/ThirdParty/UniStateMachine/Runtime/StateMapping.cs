/*
 * Copyright (c) 2019 Made With Monster Love (Pty) Ltd
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the 
 * Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included 
 * in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR 
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR 
 * OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections;

namespace MG
{
	internal class StateMapping<TState, TDriver> where TState : struct, IConvertible, IComparable
		where TDriver : class, new()
	{
		public TState state;

		public bool hasEnterRoutine;
		public Action EnterCall = DoNothing;
		public Func<IEnumerator> EnterRoutine = DoNothingCoroutine;

		public bool hasExitRoutine;
		public Action ExitCall = DoNothing;
		public Func<IEnumerator> ExitRoutine = DoNothingCoroutine;

		public Action Finally = DoNothing;

		private Func<TState> stateProviderCallback;
		private StateMachine<TState, TDriver> fsm;

		public StateMapping(StateMachine<TState, TDriver> fsm, TState state, Func<TState> stateProvider)
		{
			this.fsm = fsm;
			this.state = state;
			stateProviderCallback = stateProvider;
		}

        public static void DoNothing()
        {
        }

        public static IEnumerator DoNothingCoroutine()
        {
            yield break;
        }
    }
}
