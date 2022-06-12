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
    #region DataType Identifiers
    /// <summary>
    /// A class that declares constants for all DataTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class DataTypes
    {
        /// <summary>
        /// The identifier for the OrderStateEnum DataType.
        /// </summary>
        public const uint OrderStateEnum = 16;
    }
    #endregion

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

        /// <summary>
        /// The identifier for the Offer ObjectType.
        /// </summary>
        public const uint Offer = 6;

        /// <summary>
        /// The identifier for the OfferChoice ObjectType.
        /// </summary>
        public const uint OfferChoice = 11;

        /// <summary>
        /// The identifier for the ShopCart ObjectType.
        /// </summary>
        public const uint ShopCart = 15;

        /// <summary>
        /// The identifier for the Order ObjectType.
        /// </summary>
        public const uint Order = 18;
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

        /// <summary>
        /// The identifier for the Offer_offerId Variable.
        /// </summary>
        public const uint Offer_offerId = 7;

        /// <summary>
        /// The identifier for the Offer_offerName Variable.
        /// </summary>
        public const uint Offer_offerName = 8;

        /// <summary>
        /// The identifier for the Offer_offerDescription Variable.
        /// </summary>
        public const uint Offer_offerDescription = 9;

        /// <summary>
        /// The identifier for the Offer_offerSellPrice Variable.
        /// </summary>
        public const uint Offer_offerSellPrice = 10;

        /// <summary>
        /// The identifier for the Offer_offerCount Variable.
        /// </summary>
        public const uint Offer_offerCount = 28;

        /// <summary>
        /// The identifier for the OfferChoice_offerChoiceId Variable.
        /// </summary>
        public const uint OfferChoice_offerChoiceId = 12;

        /// <summary>
        /// The identifier for the OfferChoice_offerChoiceOfferId Variable.
        /// </summary>
        public const uint OfferChoice_offerChoiceOfferId = 13;

        /// <summary>
        /// The identifier for the OfferChoice_offerChoiceCount Variable.
        /// </summary>
        public const uint OfferChoice_offerChoiceCount = 14;

        /// <summary>
        /// The identifier for the OrderStateEnum_EnumStrings Variable.
        /// </summary>
        public const uint OrderStateEnum_EnumStrings = 17;

        /// <summary>
        /// The identifier for the Order_orderId Variable.
        /// </summary>
        public const uint Order_orderId = 19;

        /// <summary>
        /// The identifier for the Order_orderOfferChoiceIds Variable.
        /// </summary>
        public const uint Order_orderOfferChoiceIds = 20;

        /// <summary>
        /// The identifier for the Order_orderCreationTime Variable.
        /// </summary>
        public const uint Order_orderCreationTime = 21;

        /// <summary>
        /// The identifier for the Order_orderState Variable.
        /// </summary>
        public const uint Order_orderState = 29;

        /// <summary>
        /// The identifier for the cas_XmlSchema Variable.
        /// </summary>
        public const uint cas_XmlSchema = 22;

        /// <summary>
        /// The identifier for the cas_XmlSchema_NamespaceUri Variable.
        /// </summary>
        public const uint cas_XmlSchema_NamespaceUri = 24;

        /// <summary>
        /// The identifier for the cas_BinarySchema Variable.
        /// </summary>
        public const uint cas_BinarySchema = 25;

        /// <summary>
        /// The identifier for the cas_BinarySchema_NamespaceUri Variable.
        /// </summary>
        public const uint cas_BinarySchema_NamespaceUri = 27;
    }
    #endregion

    #region DataType Node Identifiers
    /// <summary>
    /// A class that declares constants for all DataTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class DataTypeIds
    {
        /// <summary>
        /// The identifier for the OrderStateEnum DataType.
        /// </summary>
        public static readonly ExpandedNodeId OrderStateEnum = new ExpandedNodeId(cas.DataTypes.OrderStateEnum, cas.Namespaces.cas);
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

        /// <summary>
        /// The identifier for the Offer ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId Offer = new ExpandedNodeId(cas.ObjectTypes.Offer, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the OfferChoice ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId OfferChoice = new ExpandedNodeId(cas.ObjectTypes.OfferChoice, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the ShopCart ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId ShopCart = new ExpandedNodeId(cas.ObjectTypes.ShopCart, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the Order ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId Order = new ExpandedNodeId(cas.ObjectTypes.Order, cas.Namespaces.cas);
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

        /// <summary>
        /// The identifier for the Offer_offerId Variable.
        /// </summary>
        public static readonly ExpandedNodeId Offer_offerId = new ExpandedNodeId(cas.Variables.Offer_offerId, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the Offer_offerName Variable.
        /// </summary>
        public static readonly ExpandedNodeId Offer_offerName = new ExpandedNodeId(cas.Variables.Offer_offerName, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the Offer_offerDescription Variable.
        /// </summary>
        public static readonly ExpandedNodeId Offer_offerDescription = new ExpandedNodeId(cas.Variables.Offer_offerDescription, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the Offer_offerSellPrice Variable.
        /// </summary>
        public static readonly ExpandedNodeId Offer_offerSellPrice = new ExpandedNodeId(cas.Variables.Offer_offerSellPrice, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the Offer_offerCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId Offer_offerCount = new ExpandedNodeId(cas.Variables.Offer_offerCount, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the OfferChoice_offerChoiceId Variable.
        /// </summary>
        public static readonly ExpandedNodeId OfferChoice_offerChoiceId = new ExpandedNodeId(cas.Variables.OfferChoice_offerChoiceId, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the OfferChoice_offerChoiceOfferId Variable.
        /// </summary>
        public static readonly ExpandedNodeId OfferChoice_offerChoiceOfferId = new ExpandedNodeId(cas.Variables.OfferChoice_offerChoiceOfferId, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the OfferChoice_offerChoiceCount Variable.
        /// </summary>
        public static readonly ExpandedNodeId OfferChoice_offerChoiceCount = new ExpandedNodeId(cas.Variables.OfferChoice_offerChoiceCount, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the OrderStateEnum_EnumStrings Variable.
        /// </summary>
        public static readonly ExpandedNodeId OrderStateEnum_EnumStrings = new ExpandedNodeId(cas.Variables.OrderStateEnum_EnumStrings, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the Order_orderId Variable.
        /// </summary>
        public static readonly ExpandedNodeId Order_orderId = new ExpandedNodeId(cas.Variables.Order_orderId, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the Order_orderOfferChoiceIds Variable.
        /// </summary>
        public static readonly ExpandedNodeId Order_orderOfferChoiceIds = new ExpandedNodeId(cas.Variables.Order_orderOfferChoiceIds, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the Order_orderCreationTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId Order_orderCreationTime = new ExpandedNodeId(cas.Variables.Order_orderCreationTime, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the Order_orderState Variable.
        /// </summary>
        public static readonly ExpandedNodeId Order_orderState = new ExpandedNodeId(cas.Variables.Order_orderState, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_XmlSchema Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_XmlSchema = new ExpandedNodeId(cas.Variables.cas_XmlSchema, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_XmlSchema_NamespaceUri Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_XmlSchema_NamespaceUri = new ExpandedNodeId(cas.Variables.cas_XmlSchema_NamespaceUri, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_BinarySchema Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_BinarySchema = new ExpandedNodeId(cas.Variables.cas_BinarySchema, cas.Namespaces.cas);

        /// <summary>
        /// The identifier for the cas_BinarySchema_NamespaceUri Variable.
        /// </summary>
        public static readonly ExpandedNodeId cas_BinarySchema_NamespaceUri = new ExpandedNodeId(cas.Variables.cas_BinarySchema_NamespaceUri, cas.Namespaces.cas);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    public static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the cas_BinarySchema component.
        /// </summary>
        public const string cas_BinarySchema = "cas";

        /// <summary>
        /// The BrowseName for the cas_XmlSchema component.
        /// </summary>
        public const string cas_XmlSchema = "cas";

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

        /// <summary>
        /// The BrowseName for the Offer component.
        /// </summary>
        public const string Offer = "Offer";

        /// <summary>
        /// The BrowseName for the OfferChoice component.
        /// </summary>
        public const string OfferChoice = "OfferChoice";

        /// <summary>
        /// The BrowseName for the offerChoiceCount component.
        /// </summary>
        public const string offerChoiceCount = "offerChoiceCount";

        /// <summary>
        /// The BrowseName for the offerChoiceId component.
        /// </summary>
        public const string offerChoiceId = "offerChoiceId";

        /// <summary>
        /// The BrowseName for the offerChoiceOfferId component.
        /// </summary>
        public const string offerChoiceOfferId = "offerChoiceOfferId";

        /// <summary>
        /// The BrowseName for the offerCount component.
        /// </summary>
        public const string offerCount = "offerCount";

        /// <summary>
        /// The BrowseName for the offerDescription component.
        /// </summary>
        public const string offerDescription = "offerDescription";

        /// <summary>
        /// The BrowseName for the offerId component.
        /// </summary>
        public const string offerId = "offerId";

        /// <summary>
        /// The BrowseName for the offerName component.
        /// </summary>
        public const string offerName = "offerName";

        /// <summary>
        /// The BrowseName for the offerSellPrice component.
        /// </summary>
        public const string offerSellPrice = "offerSellPrice";

        /// <summary>
        /// The BrowseName for the Order component.
        /// </summary>
        public const string Order = "Order";

        /// <summary>
        /// The BrowseName for the orderCreationTime component.
        /// </summary>
        public const string orderCreationTime = "orderCreationTime";

        /// <summary>
        /// The BrowseName for the orderId component.
        /// </summary>
        public const string orderId = "orderId";

        /// <summary>
        /// The BrowseName for the orderOfferChoiceIds component.
        /// </summary>
        public const string orderOfferChoiceIds = "orderOfferChoiceIds";

        /// <summary>
        /// The BrowseName for the orderState component.
        /// </summary>
        public const string orderState = "orderState";

        /// <summary>
        /// The BrowseName for the OrderStateEnum component.
        /// </summary>
        public const string OrderStateEnum = "OrderStateEnum";

        /// <summary>
        /// The BrowseName for the ShopCart component.
        /// </summary>
        public const string ShopCart = "ShopCart";
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

    #region OfferState Class
    #if (!OPCUA_EXCLUDE_OfferState)
    /// <summary>
    /// Stores an instance of the Offer ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class OfferState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public OfferState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(cas.ObjectTypes.Offer, cas.Namespaces.cas, namespaceUris);
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
           "AQAAABwAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIv/////wRggAABAAAAAQANAAAAT2ZmZXJJ" +
           "bnN0YW5jZQEBBgABAQYA/////wUAAAAVYIkKAgAAAAEABwAAAG9mZmVySWQBAQcAAC8APwcAAAAABv//" +
           "//8BAf////8AAAAAFWCJCgIAAAABAAkAAABvZmZlck5hbWUBAQgAAC8APwgAAAAADP////8BAf////8A" +
           "AAAAFWCJCgIAAAABABAAAABvZmZlckRlc2NyaXB0aW9uAQEJAAAvAD8JAAAAAAz/////AQH/////AAAA" +
           "ABVgiQoCAAAAAQAOAAAAb2ZmZXJTZWxsUHJpY2UBAQoAAC8APwoAAAAABv////8BAf////8AAAAAFWCJ" +
           "CgIAAAABAAoAAABvZmZlckNvdW50AQEcAAAvAD8cAAAAAAb/////AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the offerId Variable.
        /// </summary>
        public BaseDataVariableState<int> offerId
        {
            get
            {
                return m_offerId;
            }

            set
            {
                if (!Object.ReferenceEquals(m_offerId, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_offerId = value;
            }
        }

        /// <summary>
        /// A description for the offerName Variable.
        /// </summary>
        public BaseDataVariableState<string> offerName
        {
            get
            {
                return m_offerName;
            }

            set
            {
                if (!Object.ReferenceEquals(m_offerName, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_offerName = value;
            }
        }

        /// <summary>
        /// A description for the offerDescription Variable.
        /// </summary>
        public BaseDataVariableState<string> offerDescription
        {
            get
            {
                return m_offerDescription;
            }

            set
            {
                if (!Object.ReferenceEquals(m_offerDescription, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_offerDescription = value;
            }
        }

        /// <summary>
        /// A description for the offerSellPrice Variable.
        /// </summary>
        public BaseDataVariableState<int> offerSellPrice
        {
            get
            {
                return m_offerSellPrice;
            }

            set
            {
                if (!Object.ReferenceEquals(m_offerSellPrice, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_offerSellPrice = value;
            }
        }

        /// <summary>
        /// A description for the offerCount Variable.
        /// </summary>
        public BaseDataVariableState<int> offerCount
        {
            get
            {
                return m_offerCount;
            }

            set
            {
                if (!Object.ReferenceEquals(m_offerCount, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_offerCount = value;
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
            if (m_offerId != null)
            {
                children.Add(m_offerId);
            }

            if (m_offerName != null)
            {
                children.Add(m_offerName);
            }

            if (m_offerDescription != null)
            {
                children.Add(m_offerDescription);
            }

            if (m_offerSellPrice != null)
            {
                children.Add(m_offerSellPrice);
            }

            if (m_offerCount != null)
            {
                children.Add(m_offerCount);
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
                case cas.BrowseNames.offerId:
                {
                    if (createOrReplace)
                    {
                        if (offerId == null)
                        {
                            if (replacement == null)
                            {
                                offerId = new BaseDataVariableState<int>(this);
                            }
                            else
                            {
                                offerId = (BaseDataVariableState<int>)replacement;
                            }
                        }
                    }

                    instance = offerId;
                    break;
                }

                case cas.BrowseNames.offerName:
                {
                    if (createOrReplace)
                    {
                        if (offerName == null)
                        {
                            if (replacement == null)
                            {
                                offerName = new BaseDataVariableState<string>(this);
                            }
                            else
                            {
                                offerName = (BaseDataVariableState<string>)replacement;
                            }
                        }
                    }

                    instance = offerName;
                    break;
                }

                case cas.BrowseNames.offerDescription:
                {
                    if (createOrReplace)
                    {
                        if (offerDescription == null)
                        {
                            if (replacement == null)
                            {
                                offerDescription = new BaseDataVariableState<string>(this);
                            }
                            else
                            {
                                offerDescription = (BaseDataVariableState<string>)replacement;
                            }
                        }
                    }

                    instance = offerDescription;
                    break;
                }

                case cas.BrowseNames.offerSellPrice:
                {
                    if (createOrReplace)
                    {
                        if (offerSellPrice == null)
                        {
                            if (replacement == null)
                            {
                                offerSellPrice = new BaseDataVariableState<int>(this);
                            }
                            else
                            {
                                offerSellPrice = (BaseDataVariableState<int>)replacement;
                            }
                        }
                    }

                    instance = offerSellPrice;
                    break;
                }

                case cas.BrowseNames.offerCount:
                {
                    if (createOrReplace)
                    {
                        if (offerCount == null)
                        {
                            if (replacement == null)
                            {
                                offerCount = new BaseDataVariableState<int>(this);
                            }
                            else
                            {
                                offerCount = (BaseDataVariableState<int>)replacement;
                            }
                        }
                    }

                    instance = offerCount;
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
        private BaseDataVariableState<int> m_offerId;
        private BaseDataVariableState<string> m_offerName;
        private BaseDataVariableState<string> m_offerDescription;
        private BaseDataVariableState<int> m_offerSellPrice;
        private BaseDataVariableState<int> m_offerCount;
        #endregion
    }
    #endif
    #endregion

    #region OfferChoiceState Class
    #if (!OPCUA_EXCLUDE_OfferChoiceState)
    /// <summary>
    /// Stores an instance of the OfferChoice ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class OfferChoiceState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public OfferChoiceState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(cas.ObjectTypes.OfferChoice, cas.Namespaces.cas, namespaceUris);
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
           "AQAAABwAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIv/////wRggAABAAAAAQATAAAAT2ZmZXJD" +
           "aG9pY2VJbnN0YW5jZQEBCwABAQsA/////wMAAAAVYIkKAgAAAAEADQAAAG9mZmVyQ2hvaWNlSWQBAQwA" +
           "AC8APwwAAAAABv////8BAf////8AAAAAFWCJCgIAAAABABIAAABvZmZlckNob2ljZU9mZmVySWQBAQ0A" +
           "AC8APw0AAAAABv////8BAf////8AAAAAFWCJCgIAAAABABAAAABvZmZlckNob2ljZUNvdW50AQEOAAAv" +
           "AD8OAAAAAAb/////AQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the offerChoiceId Variable.
        /// </summary>
        public BaseDataVariableState<int> offerChoiceId
        {
            get
            {
                return m_offerChoiceId;
            }

            set
            {
                if (!Object.ReferenceEquals(m_offerChoiceId, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_offerChoiceId = value;
            }
        }

        /// <summary>
        /// A description for the offerChoiceOfferId Variable.
        /// </summary>
        public BaseDataVariableState<int> offerChoiceOfferId
        {
            get
            {
                return m_offerChoiceOfferId;
            }

            set
            {
                if (!Object.ReferenceEquals(m_offerChoiceOfferId, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_offerChoiceOfferId = value;
            }
        }

        /// <summary>
        /// A description for the offerChoiceCount Variable.
        /// </summary>
        public BaseDataVariableState<int> offerChoiceCount
        {
            get
            {
                return m_offerChoiceCount;
            }

            set
            {
                if (!Object.ReferenceEquals(m_offerChoiceCount, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_offerChoiceCount = value;
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
            if (m_offerChoiceId != null)
            {
                children.Add(m_offerChoiceId);
            }

            if (m_offerChoiceOfferId != null)
            {
                children.Add(m_offerChoiceOfferId);
            }

            if (m_offerChoiceCount != null)
            {
                children.Add(m_offerChoiceCount);
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
                case cas.BrowseNames.offerChoiceId:
                {
                    if (createOrReplace)
                    {
                        if (offerChoiceId == null)
                        {
                            if (replacement == null)
                            {
                                offerChoiceId = new BaseDataVariableState<int>(this);
                            }
                            else
                            {
                                offerChoiceId = (BaseDataVariableState<int>)replacement;
                            }
                        }
                    }

                    instance = offerChoiceId;
                    break;
                }

                case cas.BrowseNames.offerChoiceOfferId:
                {
                    if (createOrReplace)
                    {
                        if (offerChoiceOfferId == null)
                        {
                            if (replacement == null)
                            {
                                offerChoiceOfferId = new BaseDataVariableState<int>(this);
                            }
                            else
                            {
                                offerChoiceOfferId = (BaseDataVariableState<int>)replacement;
                            }
                        }
                    }

                    instance = offerChoiceOfferId;
                    break;
                }

                case cas.BrowseNames.offerChoiceCount:
                {
                    if (createOrReplace)
                    {
                        if (offerChoiceCount == null)
                        {
                            if (replacement == null)
                            {
                                offerChoiceCount = new BaseDataVariableState<int>(this);
                            }
                            else
                            {
                                offerChoiceCount = (BaseDataVariableState<int>)replacement;
                            }
                        }
                    }

                    instance = offerChoiceCount;
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
        private BaseDataVariableState<int> m_offerChoiceId;
        private BaseDataVariableState<int> m_offerChoiceOfferId;
        private BaseDataVariableState<int> m_offerChoiceCount;
        #endregion
    }
    #endif
    #endregion

    #region ShopCartState Class
    #if (!OPCUA_EXCLUDE_ShopCartState)
    /// <summary>
    /// Stores an instance of the ShopCart ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class ShopCartState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public ShopCartState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(cas.ObjectTypes.ShopCart, cas.Namespaces.cas, namespaceUris);
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
           "AQAAABwAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIv/////wRggAABAAAAAQAQAAAAU2hvcENh" +
           "cnRJbnN0YW5jZQEBDwABAQ8A/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region OrderStateEnum Enumeration
    #if (!OPCUA_EXCLUDE_OrderStateEnum)
    /// <summary>
    /// A description for the OrderStateEnum DataType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = cas.Namespaces.casXsd)]
    public enum OrderStateEnum
    {
        /// <summary>
        /// A description for the WAITING field.
        /// </summary>
        [EnumMember(Value = "WAITING_0")]
        WAITING = 0,

        /// <summary>
        /// A description for the PREPARED field.
        /// </summary>
        [EnumMember(Value = "PREPARED_1")]
        PREPARED = 1,

        /// <summary>
        /// A description for the SENT field.
        /// </summary>
        [EnumMember(Value = "SENT_2")]
        SENT = 2,

        /// <summary>
        /// A description for the FULFILLED field.
        /// </summary>
        [EnumMember(Value = "FULFILLED_3")]
        FULFILLED = 3,
    }

    #region OrderStateEnumCollection Class
    /// <summary>
    /// A collection of OrderStateEnum objects.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfOrderStateEnum", Namespace = cas.Namespaces.casXsd, ItemName = "OrderStateEnum")]
    #if !NET_STANDARD
    public partial class OrderStateEnumCollection : List<OrderStateEnum>, ICloneable
    #else
    public partial class OrderStateEnumCollection : List<OrderStateEnum>
    #endif
    {
        #region Constructors
        /// <summary>
        /// Initializes the collection with default values.
        /// </summary>
        public OrderStateEnumCollection() {}

        /// <summary>
        /// Initializes the collection with an initial capacity.
        /// </summary>
        public OrderStateEnumCollection(int capacity) : base(capacity) {}

        /// <summary>
        /// Initializes the collection with another collection.
        /// </summary>
        public OrderStateEnumCollection(IEnumerable<OrderStateEnum> collection) : base(collection) {}
        #endregion

        #region Static Operators
        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        public static implicit operator OrderStateEnumCollection(OrderStateEnum[] values)
        {
            if (values != null)
            {
                return new OrderStateEnumCollection(values);
            }

            return new OrderStateEnumCollection();
        }

        /// <summary>
        /// Converts a collection to an array.
        /// </summary>
        public static explicit operator OrderStateEnum[](OrderStateEnumCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #if !NET_STANDARD
        #region ICloneable Methods
        /// <summary>
        /// Creates a deep copy of the collection.
        /// </summary>
        public object Clone()
        {
            return (OrderStateEnumCollection)this.MemberwiseClone();
        }
        #endregion
        #endif

        /// <summary cref="Object.MemberwiseClone" />
        public new object MemberwiseClone()
        {
            OrderStateEnumCollection clone = new OrderStateEnumCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((OrderStateEnum)Utils.Clone(this[ii]));
            }

            return clone;
        }
    }
    #endregion
    #endif
    #endregion

    #region OrderState Class
    #if (!OPCUA_EXCLUDE_OrderState)
    /// <summary>
    /// Stores an instance of the Order ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class OrderState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public OrderState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(cas.ObjectTypes.Order, cas.Namespaces.cas, namespaceUris);
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
           "AQAAABwAAABodHRwOi8vY2FzLmV1L1VBL0NvbW1TZXJ2ZXIv/////wRggAABAAAAAQANAAAAT3JkZXJJ" +
           "bnN0YW5jZQEBEgABARIA/////wQAAAAVYIkKAgAAAAEABwAAAG9yZGVySWQBARMAAC8APxMAAAAABv//" +
           "//8BAf////8AAAAAFWCJCgIAAAABABMAAABvcmRlck9mZmVyQ2hvaWNlSWRzAQEUAAAvAD8UAAAAAAYB" +
           "AAAAAQH/////AAAAABVgiQoCAAAAAQARAAAAb3JkZXJDcmVhdGlvblRpbWUBARUAAC8APxUAAAAADf//" +
           "//8BAf////8AAAAAFWCJCgIAAAABAAoAAABvcmRlclN0YXRlAQEdAAAvAD8dAAAAAQEQAP////8BAf//" +
           "//8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the orderId Variable.
        /// </summary>
        public BaseDataVariableState<int> orderId
        {
            get
            {
                return m_orderId;
            }

            set
            {
                if (!Object.ReferenceEquals(m_orderId, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_orderId = value;
            }
        }

        /// <summary>
        /// A description for the orderOfferChoiceIds Variable.
        /// </summary>
        public BaseDataVariableState<int[]> orderOfferChoiceIds
        {
            get
            {
                return m_orderOfferChoiceIds;
            }

            set
            {
                if (!Object.ReferenceEquals(m_orderOfferChoiceIds, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_orderOfferChoiceIds = value;
            }
        }

        /// <summary>
        /// A description for the orderCreationTime Variable.
        /// </summary>
        public BaseDataVariableState<DateTime> orderCreationTime
        {
            get
            {
                return m_orderCreationTime;
            }

            set
            {
                if (!Object.ReferenceEquals(m_orderCreationTime, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_orderCreationTime = value;
            }
        }

        /// <summary>
        /// A description for the orderState Variable.
        /// </summary>
        public BaseDataVariableState<OrderStateEnum> orderState
        {
            get
            {
                return m_orderState;
            }

            set
            {
                if (!Object.ReferenceEquals(m_orderState, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_orderState = value;
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
            if (m_orderId != null)
            {
                children.Add(m_orderId);
            }

            if (m_orderOfferChoiceIds != null)
            {
                children.Add(m_orderOfferChoiceIds);
            }

            if (m_orderCreationTime != null)
            {
                children.Add(m_orderCreationTime);
            }

            if (m_orderState != null)
            {
                children.Add(m_orderState);
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
                case cas.BrowseNames.orderId:
                {
                    if (createOrReplace)
                    {
                        if (orderId == null)
                        {
                            if (replacement == null)
                            {
                                orderId = new BaseDataVariableState<int>(this);
                            }
                            else
                            {
                                orderId = (BaseDataVariableState<int>)replacement;
                            }
                        }
                    }

                    instance = orderId;
                    break;
                }

                case cas.BrowseNames.orderOfferChoiceIds:
                {
                    if (createOrReplace)
                    {
                        if (orderOfferChoiceIds == null)
                        {
                            if (replacement == null)
                            {
                                orderOfferChoiceIds = new BaseDataVariableState<int[]>(this);
                            }
                            else
                            {
                                orderOfferChoiceIds = (BaseDataVariableState<int[]>)replacement;
                            }
                        }
                    }

                    instance = orderOfferChoiceIds;
                    break;
                }

                case cas.BrowseNames.orderCreationTime:
                {
                    if (createOrReplace)
                    {
                        if (orderCreationTime == null)
                        {
                            if (replacement == null)
                            {
                                orderCreationTime = new BaseDataVariableState<DateTime>(this);
                            }
                            else
                            {
                                orderCreationTime = (BaseDataVariableState<DateTime>)replacement;
                            }
                        }
                    }

                    instance = orderCreationTime;
                    break;
                }

                case cas.BrowseNames.orderState:
                {
                    if (createOrReplace)
                    {
                        if (orderState == null)
                        {
                            if (replacement == null)
                            {
                                orderState = new BaseDataVariableState<OrderStateEnum>(this);
                            }
                            else
                            {
                                orderState = (BaseDataVariableState<OrderStateEnum>)replacement;
                            }
                        }
                    }

                    instance = orderState;
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
        private BaseDataVariableState<int> m_orderId;
        private BaseDataVariableState<int[]> m_orderOfferChoiceIds;
        private BaseDataVariableState<DateTime> m_orderCreationTime;
        private BaseDataVariableState<OrderStateEnum> m_orderState;
        #endregion
    }
    #endif
    #endregion
}