/* ========================================================================
 * Copyright (c) 2005-2016 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using Opc.Ua;

namespace cas
{
    #region ObjectType Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypes
    {
        /// <summary>
        /// The identifier for the Client ObjectType.
        /// </summary>
        public const uint Client = 1;
    }
    #endregion

    #region Variable Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Variables
    {
        /// <summary>
        /// The identifier for the Client_clientId Variable.
        /// </summary>
        public const uint Client_clientId = 3;

        /// <summary>
        /// The identifier for the Client_clientName Variable.
        /// </summary>
        public const uint Client_clientName = 4;

        /// <summary>
        /// The identifier for the Client_clientPassword Variable.
        /// </summary>
        public const uint Client_clientPassword = 5;
    }
    #endregion

    #region ObjectType Node Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypeIds
    {
        /// <summary>
        /// The identifier for the Client ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId Client = new ExpandedNodeId(cas.ObjectTypes.Client, cas.Namespaces.cas);
    }
    #endregion

    #region Variable Node Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableIds
    {
        /// <summary>
        /// The identifier for the Client_clientId Variable.
        /// </summary>
        public static readonly ExpandedNodeId Client_clientId = new ExpandedNodeId(cas.Variables.Client_clientId, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the Client_clientName Variable.
        /// </summary>
        public static readonly ExpandedNodeId Client_clientName = new ExpandedNodeId(cas.Variables.Client_clientName, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the Client_clientPassword Variable.
        /// </summary>
        public static readonly ExpandedNodeId Client_clientPassword = new ExpandedNodeId(cas.Variables.Client_clientPassword, cas.Namespaces.cas);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    public static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the Client component.
        /// </summary>
        public const string Client = "Client";

        /// <summary>
        /// The BrowseName for the clientId component.
        /// </summary>
        public const string clientId = "clientId";

        /// <summary>
        /// The BrowseName for the clientName component.
        /// </summary>
        public const string clientName = "clientName";

        /// <summary>
        /// The BrowseName for the clientPassword component.
        /// </summary>
        public const string clientPassword = "clientPassword";
    }
    #endregion

    #region Namespace Declarations
    /// <summary>
    /// Defines constants for all namespaces referenced by the model design.
    /// </summary>
    public static partial class Namespaces
    {
        /// <summary>
        /// The URI for the cas namespace (.NET code namespace is 'cas').
        /// </summary>
        public const string cas = "http://cas.eu/UA/CommServer/";

        /// <summary>
        /// The URI for the casXsd namespace (.NET code namespace is 'cas').
        /// </summary>
        public const string casXsd = "cas";

        /// <summary>
        /// The URI for the OpcUa namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUa = "http://opcfoundation.org/UA/";

        /// <summary>
        /// The URI for the OpcUaXsd namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUaXsd = "http://opcfoundation.org/UA/2008/02/Types.xsd";
    }
    #endregion

    #region ClientState Class
    #if (!OPCUA_EXCLUDE_ClientState)
    /// <summary>
    /// Stores an instance of the Client ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class ClientState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public ClientState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(cas.ObjectTypes.Client, cas.Namespaces.cas, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AQAAABwAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIv/////wRggAABAAAAAQAOAAAAQ2xpZW50" +
           "SW5zdGFuY2UBAQEAAQEBAP////8DAAAAFWCJCgIAAAABAAgAAABjbGllbnRJZAEBAwAALwA/AwAAAAAG" +
           "/////wEB/////wAAAAAVYIkKAgAAAAEACgAAAGNsaWVudE5hbWUBAQQAAC8APwQAAAAADP////8BAf//" +
           "//8AAAAAFWCJCgIAAAABAA4AAABjbGllbnRQYXNzd29yZAEBBQAALwA/BQAAAAAM/////wEB/////wAA" +
           "AAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the clientId Variable.
        /// </summary>
        public BaseDataVariableState<int> clientId
        {
            get
            {
                return m_clientId;
            }

            set
            {
                if (!Object.ReferenceEquals(m_clientId, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_clientId = value;
            }
        }

        /// <summary>
        /// A description for the clientName Variable.
        /// </summary>
        public BaseDataVariableState<string> clientName
        {
            get
            {
                return m_clientName;
            }

            set
            {
                if (!Object.ReferenceEquals(m_clientName, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_clientName = value;
            }
        }

        /// <summary>
        /// A description for the clientPassword Variable.
        /// </summary>
        public BaseDataVariableState<string> clientPassword
        {
            get
            {
                return m_clientPassword;
            }

            set
            {
                if (!Object.ReferenceEquals(m_clientPassword, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_clientPassword = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_clientId != null)
            {
                children.Add(m_clientId);
            }

            if (m_clientName != null)
            {
                children.Add(m_clientName);
            }

            if (m_clientPassword != null)
            {
                children.Add(m_clientPassword);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case cas.BrowseNames.clientId:
                {
                    if (createOrReplace)
                    {
                        if (clientId == null)
                        {
                            if (replacement == null)
                            {
                                clientId = new BaseDataVariableState<int>(this);
                            }
                            else
                            {
                                clientId = (BaseDataVariableState<int>)replacement;
                            }
                        }
                    }

                    instance = clientId;
                    break;
                }

                case cas.BrowseNames.clientName:
                {
                    if (createOrReplace)
                    {
                        if (clientName == null)
                        {
                            if (replacement == null)
                            {
                                clientName = new BaseDataVariableState<string>(this);
                            }
                            else
                            {
                                clientName = (BaseDataVariableState<string>)replacement;
                            }
                        }
                    }

                    instance = clientName;
                    break;
                }

                case cas.BrowseNames.clientPassword:
                {
                    if (createOrReplace)
                    {
                        if (clientPassword == null)
                        {
                            if (replacement == null)
                            {
                                clientPassword = new BaseDataVariableState<string>(this);
                            }
                            else
                            {
                                clientPassword = (BaseDataVariableState<string>)replacement;
                            }
                        }
                    }

                    instance = clientPassword;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private BaseDataVariableState<int> m_clientId;
        private BaseDataVariableState<string> m_clientName;
        private BaseDataVariableState<string> m_clientPassword;
        #endregion
    }
    #endif
    #endregion
}