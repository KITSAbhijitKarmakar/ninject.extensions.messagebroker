#region License

// Author: Nate Kohari <nate@enkari.com>
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

#endregion

#region Using Directives

using System;
using Ninject.Infrastructure.Disposal;

#endregion

namespace Ninject.Extensions.MessageBroker
{
    internal static class Ensure
    {
        public static void ArgumentNotNull( object argument, string name )
        {
            if ( argument == null )
            {
                throw new ArgumentNullException( name, "Cannot be null" );
            }
        }

        public static void ArgumentNotNullOrEmpty( string argument, string name )
        {
            if ( String.IsNullOrEmpty( argument ) )
            {
                throw new ArgumentException( "Cannot be null or empty", name );
            }
        }

        /// <summary>
        /// Throws an exception if the specified object has been disposed.
        /// </summary>
        /// <param name="obj">The object in question.</param>
        public static void NotDisposed( DisposableObject obj )
        {
            if ( obj.IsDisposed )
            {
                throw new ObjectDisposedException( obj.GetType().Name );
            }
        }
    }
}