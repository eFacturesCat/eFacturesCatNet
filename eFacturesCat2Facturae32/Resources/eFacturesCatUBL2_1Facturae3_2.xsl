<?xml version="1.0" encoding="ISO-8859-1"?>
<!--
	$Id: eFacturesCatUBL2_1Facturae3_2.xsl,v 1.0 2012/03/27 10:15:21 Santi Casas $
	
	Conversión de facturas en formato UBL 2.1 a formato Facturae 3.2
-->
<!--
	Basado en el trabajo realizado previamente por Oriol Bausa de Invinet (Thx!!)

-->
<!--  xmlns:ubl="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2" -->
<xsl:stylesheet version="1.0" 
	xmlns:ubl="urn:eFacturesCat:specification:schema:xsd:eFacturesCat-Invoice"
	xmlns:facturae="http://www.facturae.es/Facturae/2009/v3.2/Facturae" 
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
	xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2" 
	xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2" 
	xmlns:ccts="urn:un:unece:uncefact:documentation:2" 
	xmlns:ext="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2" 
	xmlns:qdt="urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2" 
	xmlns:udt="urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2" 
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
	xmlns:ds="http://www.w3.org/2000/09/xmldsig#"  
	xmlns:gc="http://genericode.org/2006/ns/CodeList/0.4/"
	exclude-result-prefixes="ubl cac cbc ccts ext qdt udt ds gc">
	<xsl:import href="eFacturesCat2Facturae32.Resources.eFacturesCatUBL2_1Facturae3_2-cac.xsl"/>
	<xsl:import href="eFacturesCat2Facturae32.Resources.eFacturesCatUBL2_1Facturae3_2-cbc.xsl"/>
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>
	
	<xsl:template match="/">
    <facturae:Facturae>
      <xsl:element name="FileHeader">
        <xsl:element name="SchemaVersion">3.2</xsl:element>
        <xsl:element name="Modality">I</xsl:element>
        <xsl:if test="not(ubl:Invoice/cac:Signature)">
          <xsl:element name="InvoiceIssuerType">EM</xsl:element>
        </xsl:if>
        <xsl:apply-templates select="ubl:Invoice/cac:Signature"/>
        <xsl:element name="Batch">
          <xsl:element name="BatchIdentifier">
            <xsl:value-of select="ubl:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID"/>
            <xsl:choose>
              <xsl:when test='contains(ubl:Invoice/cbc:ID,"###")'>
                <xsl:value-of select='substring-after(ubl:Invoice/cbc:ID,"###")'/>
                <xsl:value-of select='substring-before(ubl:Invoice/cbc:ID,"###")'/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:element name="InvoiceNumber">
                  <xsl:value-of select="ubl:Invoice/cbc:ID"/>
                </xsl:element>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:element>
          <xsl:element name="InvoicesCount">1</xsl:element>
          <xsl:element name="TotalInvoicesAmount">
            <xsl:element name="TotalAmount">
              <xsl:value-of select="format-number(ubl:Invoice/cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount, '0.00')"/>
            </xsl:element>
          </xsl:element>
          <xsl:element name="TotalOutstandingAmount">
            <xsl:element name="TotalAmount">
              <xsl:value-of select="format-number(ubl:Invoice/cac:LegalMonetaryTotal/cbc:PayableAmount, '0.00')"/>
            </xsl:element>
          </xsl:element>
          <xsl:element name="TotalExecutableAmount">
            <xsl:element name="TotalAmount">
              <xsl:value-of select="format-number(ubl:Invoice/cac:LegalMonetaryTotal/cbc:PayableAmount, '0.00')"/>
            </xsl:element>
          </xsl:element>
          <xsl:element name="InvoiceCurrencyCode">
            <xsl:value-of select="ubl:Invoice/cbc:DocumentCurrencyCode"/>
          </xsl:element>
        </xsl:element>
        <!-- Por el momento no hay factoring 				
				<xsl:if test="ubl:Invoice/cac:PayeeParty">
					<xsl:element name="FactoringAssignmentData">
						<xsl:element name="Assignee">
							<xsl:apply-templates select="ubl:Invoice/cac:PayeeParty"/>
						</xsl:element>
						<xsl:apply-templates mode="factoring" select="ubl:Invoice/cac:PaymentMeans"/>
						<xsl:apply-templates mode="factoring" select="ubl:Invoice/cac:PaymentTerms/cbc:Note"/>
					</xsl:element>
				</xsl:if>
-->
      </xsl:element>
      <xsl:element name="Parties">
        <xsl:element name="SellerParty">
          <xsl:apply-templates select="ubl:Invoice/cac:AccountingSupplierParty/cac:Party"/>
        </xsl:element>
        <xsl:element name="BuyerParty">
          <xsl:apply-templates select="ubl:Invoice/cac:AccountingCustomerParty/cac:Party"/>
        </xsl:element>
      </xsl:element>
      <xsl:element name="Invoices">
        <xsl:element name="Invoice">
          <xsl:element name="InvoiceHeader">
            <xsl:apply-templates select="ubl:Invoice/cbc:ID"/>
            <xsl:apply-templates select="ubl:Invoice/cbc:InvoiceTypeCode"/>
						<!-- Not consider Copy Indicator 
						<xsl:choose>
							<xsl:when test="ubl:Invoice/cbc:CopyIndicator">
								<xsl:apply-templates select="ubl:Invoice/cbc:CopyIndicator"/>								
							</xsl:when>
							<xsl:otherwise>
								<xsl:element name="InvoiceClass">OO</xsl:element>
							</xsl:otherwise>
						</xsl:choose>
						-->
						<!-- Corrective for facturae3 -->
						<xsl:if test="ubl:Invoice/ubl:Corrective">
							<xsl:call-template name="copy-node" >
								<xsl:with-param name="node" select="ubl:Invoice/ubl:Corrective" />
							</xsl:call-template>								
						</xsl:if>
          </xsl:element>
          <xsl:element name="InvoiceIssueData">
            <xsl:apply-templates select="ubl:Invoice/cbc:IssueDate"/>
            <xsl:apply-templates select="ubl:Invoice/cbc:TaxPointDate"/>
            <!--
						<xsl:if test="ubl:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:PostalZone
											| ubl:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CityName">
							<xsl:element name="PlaceOfIssue">
								<xsl:apply-templates mode="spain" select="ubl:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:PostalZone"/>
								<xsl:apply-templates mode="PlaceOfIssueDescription" select="ubl:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CityName"/>
							</xsl:element>
						</xsl:if>
            -->
            <xsl:apply-templates select="ubl:Invoice/cac:InvoicePeriod"/>
            <xsl:apply-templates select="ubl:Invoice/cbc:DocumentCurrencyCode"/>
            <xsl:apply-templates select="ubl:Invoice/cac:TaxExchangeRate"/>
            <xsl:choose>
              <xsl:when test="ubl:Invoice/cbc:TaxCurrencyCode">
                <xsl:apply-templates select="ubl:Invoice/cbc:TaxCurrencyCode"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:element name="TaxCurrencyCode">EUR</xsl:element>
                <!-- not show element TaxCurrencyCode
									<xsl:apply-templates mode="tax" select="ubl:Invoice/cbc:DocumentCurrencyCode"/>
-->
              </xsl:otherwise>
            </xsl:choose>
            <xsl:element name="LanguageName">
              <xsl:choose>
                <xsl:when test="ubl:Invoice/cac:AccountingCustomerParty/cac:Party/cac:Language/cbc:ID">
                  <xsl:value-of select="ubl:Invoice/cac:AccountingCustomerParty/cac:Party/cac:Language/cbc:ID"/>
                </xsl:when>
                <xsl:otherwise>es</xsl:otherwise>
              </xsl:choose>
            </xsl:element>
          </xsl:element>
          <xsl:element name="TaxesOutputs">
            <xsl:apply-templates mode="withTaxAmount" select="ubl:Invoice/cac:TaxTotal/cac:TaxSubtotal[cac:TaxCategory/cac:TaxScheme/cbc:ID != 'IRPF']"/>
          </xsl:element>
          <xsl:if test="//cac:TaxTotal/cac:TaxSubtotal[cac:TaxCategory/cac:TaxScheme/cbc:ID = 'IRPF']">
            <xsl:element name="TaxesWithheld">
              <xsl:apply-templates select="ubl:Invoice/cac:TaxTotal/cac:TaxSubtotal[cac:TaxCategory/cac:TaxScheme/cbc:ID = 'IRPF']"/>
            </xsl:element>
          </xsl:if>
          <xsl:element name="InvoiceTotals">
            <xsl:apply-templates select="ubl:Invoice/cac:LegalMonetaryTotal/cbc:LineExtensionAmount"/>
            <xsl:if test="/ubl:Invoice/cac:AllowanceCharge/cbc:ChargeIndicator='false'">
              <xsl:element name="GeneralDiscounts">
                <xsl:apply-templates mode="discount" select="ubl:Invoice/cac:AllowanceCharge[cbc:ChargeIndicator='false']"/>
              </xsl:element>
            </xsl:if>
            <xsl:if test="ubl:Invoice/cac:AllowanceCharge/cbc:ChargeIndicator='true'">
              <xsl:element name="GeneralSurcharges">
                <xsl:apply-templates mode="charge" select="ubl:Invoice/cac:AllowanceCharge[cbc:ChargeIndicator='true']" />
              </xsl:element>
            </xsl:if>
            <xsl:apply-templates select="ubl:Invoice/cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount"/>
            <xsl:apply-templates select="ubl:Invoice/cac:LegalMonetaryTotal/cbc:ChargeTotalAmount"/>
            <xsl:apply-templates select="ubl:Invoice/cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount"/>
            <xsl:element name="TotalTaxOutputs">
              <xsl:value-of select="format-number(sum(/ubl:Invoice/cac:TaxTotal[cac:TaxSubtotal/cac:TaxCategory/cac:TaxScheme/cbc:ID != 'IRPF']/cbc:TaxAmount), '0.00')"/>
            </xsl:element>
            <xsl:element name="TotalTaxesWithheld">
              <xsl:value-of select="format-number(sum(/ubl:Invoice/cac:TaxTotal[cac:TaxSubtotal/cac:TaxCategory/cac:TaxScheme/cbc:ID = 'IRPF']/cbc:TaxAmount), '0.00')"/>
            </xsl:element>
            <xsl:apply-templates select="ubl:Invoice/cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount"/>
            <xsl:if test="ubl:Invoice/cac:PrepaidPayment">
              <xsl:element name="PaymentsonAccount">
                <xsl:apply-templates select="ubl:Invoice/cac:PrepaidPayment"/>
              </xsl:element>
            </xsl:if>
            <xsl:apply-templates select="ubl:Invoice/cac:LegalMonetaryTotal/cbc:PrePaidAmount"/>
            <xsl:apply-templates select="ubl:Invoice/cac:LegalMonetaryTotal/cbc:PayableAmount"/>
            <xsl:apply-templates mode="totalexecutable" select="ubl:Invoice/cac:LegalMonetaryTotal/cbc:PayableAmount"/>
          </xsl:element>
          <xsl:if test="ubl:Invoice/cac:InvoiceLine">
            <xsl:element name="Items">
              <xsl:for-each select=".">
                <xsl:apply-templates select="//cac:InvoiceLine"/>
              </xsl:for-each>
            </xsl:element>
          </xsl:if>
          <!-- old paymentmeans
					<xsl:apply-templates select="ubl:Invoice/cac:PaymentMeans"/>
					-->
          <xsl:if test="ubl:Invoice/cac:PaymentTerms">
            <xsl:element name="PaymentDetails">
              <xsl:for-each select=".">
                <xsl:apply-templates select="//cac:PaymentTerms"/>
              </xsl:for-each>
            </xsl:element>
          </xsl:if>
		<xsl:if test="ubl:Invoice/cbc:Note">
			<xsl:apply-templates mode="globalNote" select="ubl:Invoice/cbc:Note"/>
		</xsl:if>
        </xsl:element>
      </xsl:element>
    </facturae:Facturae>
	</xsl:template>
	<!--
Copy Element
 Reprocesses Element in the output document:
 Copies Attributes
 Copies Text
 Copies Child Nodes
-->
	<xsl:template name="copy-element" >
		<xsl:param name="element" />
		<xsl:if test="$element" >
			<xsl:element name="{name($element)}">
				<!-- Copy Attributes -->
				<xsl:call-template name="copy-attribute" >
					<xsl:with-param name="attribute" select="$element/@*" />
				</xsl:call-template>
				
				<!-- Copy Text -->
				<xsl:value-of select="$element/text()"/>
				
				<!-- Copy Child Nodes -->
				<xsl:for-each select="$element/*" >
					<xsl:call-template name="copy-node" >
						<xsl:with-param name="node" select="." />
					</xsl:call-template>
				</xsl:for-each>
			</xsl:element>
		</xsl:if>
	</xsl:template>
	
	<!-- Copy Attribute -->
	<xsl:template name="copy-attribute" >
		<xsl:param name="attribute" />
		<xsl:if test="$attribute" >
			<xsl:attribute name="{name($attribute)}">
				<xsl:value-of select="$attribute" />
			</xsl:attribute>
		</xsl:if>
	</xsl:template>	

  	<!-- Copy Node: Primary Call -->
	<xsl:template name="copy-node" >
		<xsl:param name="node" />
		<xsl:call-template name="copy-element" >
			<xsl:with-param name="element" select="$node" />
		</xsl:call-template>
	</xsl:template>


</xsl:stylesheet>
