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
	<xsl:import href="eFacturesCatUBL2_1Facturae3_2-cac.xsl"/>
	<xsl:import href="eFacturesCatUBL2_1Facturae3_2-cbc.xsl"/>
	<xsl:output method="xml" encoding="UTF-8" indent="yes"/>
	
	<xsl:template match="/">
		<facturae:Facturae xsi:schemaLocation="http://www.facturae.es/Facturae/2009/v3.2/Facturae http://www.facturae.es/es-ES/Documentacion/EsquemaFormato/Esquema%20Formato/Versi%C3%B3n%203_2/Facturaev3_2.xsd">
			<xsl:element name="FileHeader">
				<xsl:element name="SchemaVersion">3.2</xsl:element>
				<xsl:element name="Modality">I</xsl:element>
				<xsl:if test="not(ubl:Invoice/cac:Signature)"><xsl:element name="InvoiceIssuerType">EM</xsl:element></xsl:if>
				<xsl:apply-templates select="ubl:Invoice/cac:Signature"/>
				<xsl:element name="Batch">
					<xsl:element name="BatchIdentifier"><xsl:value-of select="ubl:Invoice/cbc:IssueDate"/><xsl:value-of select="ubl:Invoice/cbc:ID"/></xsl:element>
					<xsl:element name="InvoicesCount">1</xsl:element>
						<xsl:element name="TotalInvoicesAmount">
							<xsl:element name="TotalAmount"><xsl:value-of select="format-number(ubl:Invoice/cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount, '.00')"/></xsl:element>
						</xsl:element>
						<xsl:element name="TotalOutstandingAmount">
							<xsl:element name="TotalAmount"><xsl:value-of select="format-number(ubl:Invoice/cac:LegalMonetaryTotal/cbc:PayableAmount, '.00')"/></xsl:element>
						</xsl:element>
						<xsl:element name="TotalExecutableAmount">
							<xsl:element name="TotalAmount"><xsl:value-of select="format-number(ubl:Invoice/cac:LegalMonetaryTotal/cbc:PayableAmount, '.00')"/></xsl:element>
						</xsl:element>
						<xsl:element name="InvoiceCurrencyCode"><xsl:value-of select="ubl:Invoice/cbc:DocumentCurrencyCode"/></xsl:element>
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
						<xsl:choose>
							<xsl:when test="ubl:Invoice/cbc:CopyIndicator">
								<xsl:apply-templates select="ubl:Invoice/cbc:CopyIndicator"/>								
							</xsl:when>
							<xsl:otherwise>
								<xsl:element name="InvoiceClass">OO</xsl:element>
							</xsl:otherwise>
						</xsl:choose>
					</xsl:element>
					<xsl:element name="InvoiceIssueData">
						<xsl:apply-templates select="ubl:Invoice/cbc:IssueDate"/>
						<xsl:apply-templates select="ubl:Invoice/cbc:TaxPointDate"/>
						<xsl:if test="ubl:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:PostalZone
											| ubl:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CityName">
							<xsl:element name="PlaceOfIssue">
								<xsl:apply-templates mode="spain" select="ubl:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:PostalZone"/>
								<xsl:apply-templates mode="PlaceOfIssueDescription" select="ubl:Invoice/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress/cbc:CityName"/>
							</xsl:element>
						</xsl:if>
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
							<xsl:when test="ubl:Invoice/cac:AccountingSupplierParty/cac:Party/cac:Language/cbc:ID">
								<xsl:value-of select="ubl:Invoice/cac:AccountingSupplierParty/cac:Party/cac:Language/cbc:ID"/>
							</xsl:when>
							<xsl:otherwise>es</xsl:otherwise>
						</xsl:choose>
						</xsl:element>
					</xsl:element>
					<xsl:element name="TaxesOutputs">
						<xsl:apply-templates select="ubl:Invoice/cac:TaxTotal/cac:TaxSubtotal[cac:TaxCategory/cac:TaxScheme/cbc:ID != 'IRPF']"/>
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
							<xsl:value-of select="format-number(sum(/ubl:Invoice/cac:TaxTotal[cac:TaxSubtotal/cac:TaxCategory/cac:TaxScheme/cbc:ID != 'IRPF']/cbc:TaxAmount), '.00')"/>
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
					<xsl:apply-templates select="ubl:Invoice/cac:PaymentMeans"/>
					<xsl:if test="ubl:Invoice/cbc:Note">
						<xsl:element name="LegalLiterals">
							<xsl:apply-templates mode="legal" select="ubl:Invoice/cbc:Note"/>
						</xsl:element>
					</xsl:if>
				</xsl:element>
			</xsl:element>
		</facturae:Facturae>
	</xsl:template>
</xsl:stylesheet>
