#region License

//
// Author: Nate Kohari <nkohari@gmail.com>
// Copyright (c) 2007-2009, Enkari, Ltd.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

#region Using Directives

using System.Reflection;
using System.Text;
using Ninject.Injection;
using Ninject.Planning.Directives;

#endregion

namespace Ninject.Extensions.MessageBroker.Planning.Directives
{
    /// <summary>
    /// A directive that describes a message subscription.
    /// </summary>
    public class SubscriptionDirective : IDirective
    {
        #region Fields

        private readonly string _channel;
        private readonly MethodInjector _injector;
        private readonly DeliveryThread _thread;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the channel that is to be susbcribed to.
        /// </summary>
        public string Channel
        {
            get { return _channel; }
        }


        /// <summary>
        /// Gets the injector that triggers the method.
        /// </summary>
        public MethodInjector Injector
        {
            get { return _injector; }
        }


        /// <summary>
        /// Gets the thread on which the message should be delivered.
        /// </summary>
        public DeliveryThread Thread
        {
            get { return _thread; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionDirective"/> class.
        /// </summary>
        /// <param name="channel">The name of the channel that is to be susbcribed to.</param>
        /// <param name="injector">The injector that triggers the method.</param>
        /// <param name="thread">The thread on which the message should be delivered.</param>
        public SubscriptionDirective( string channel, MethodInjector injector, DeliveryThread thread )
        {
            Ensure.ArgumentNotNullOrEmpty( channel, "channel" );
            Ensure.ArgumentNotNull( injector, "injector" );

            _channel = channel;
            _injector = injector;
            _thread = thread;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds the value that uniquely identifies the directive. This is called the first time
        /// the key is accessed, and then cached in the directive.
        /// </summary>
        /// <returns>The directive's unique key.</returns>
        /// <remarks>
        /// This exists because most directives' keys are based on reading member information,
        /// especially parameters. Since it's a relatively expensive procedure, it shouldn't be
        /// done each time the key is accessed.
        /// </remarks>
        protected object BuildKey()
        {
            var sb = new StringBuilder();

            sb.Append( _channel );
            sb.Append( _injector.Method.Name );

            ParameterInfo[] parameters = _injector.Method.GetParameters();
            foreach ( ParameterInfo parameter in parameters )
            {
                sb.Append( parameter.ParameterType.FullName );
            }

            return sb.ToString();
        }

        #endregion
    }
}