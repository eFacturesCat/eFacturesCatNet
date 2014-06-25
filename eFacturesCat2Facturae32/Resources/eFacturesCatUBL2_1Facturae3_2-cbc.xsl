<?xml version="1.0" encoding="iso-8859-1"?>
<!--
	$Id: InvinetUBL2Facturae3-cbc.xsl,v 1.0 2008/02/22 10:15:21 Oriol Bausa $
	
	ConversiÃÂ³n de facturas en formato UBL 2.0 a formato Facturae 3.1
	ConversiÃÂ³n de Common Basic Components
-->
<!--
	Copyright (C) - Invinet Sistemes 2003, SL
	- http://www.invinet.org/

-->
<xsl:stylesheet version="1.0" xmlns:ubl="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2" 
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

	
	<!-- Contact -->
	<xsl:template match="cbc:Telephone">
		<xsl:element name="Telephone">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:Telefax">
		<xsl:element name="TeleFax">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:WebsiteURI">
		<xsl:element name="WebAddress">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:ElectronicMail">
		<xsl:element name="ElectronicMail">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:Contact/cbc:Name">
		<xsl:element name="ContactPersons">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:Contact/cbc:Note">
		<xsl:element name="AdditionalContactDetails">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:Person/cbc:FirstName">
		<xsl:element name="Name">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:Person/cbc:FamilyName">
		<xsl:element name="FirstSurname">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	
	<!-- Address -->
	<xsl:template match="cbc:StreetName">
		<xsl:element name="Address">
			<xsl:value-of select="."/>
			<xsl:text> </xsl:text>
			<xsl:value-of select="following-sibling::cbc:AdditionalStreetName"/>
			<xsl:text> </xsl:text>
			<xsl:value-of select="following-sibling::cbc:BuildingNumber"/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="spain" match="cbc:PostalZone">
		<xsl:element name="PostCode">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="overseas" match="cbc:PostalZone">
		<xsl:param name="cityName"/>
		<xsl:element name="PostCodeAndTown">
			<xsl:value-of select="."/>
			<xsl:text> </xsl:text>
			<xsl:value-of select="$cityName"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:CityName">
		<xsl:element name="Town">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:CountrySubentity | cbc:CountrySubentityCode | cbc:Region">
		<xsl:element name="Province">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:Country/cbc:IdentificationCode">
		<xsl:variable name="countryCode">
			<xsl:value-of select="."/>
		</xsl:variable>
		<xsl:element name="CountryCode">
			<xsl:value-of select="document('eFacturesCat2Facturae32.Resources.CountryIdentificationCode-2.0_facturae30.gc')/gc:CodeList/SimpleCodeList/Row[Value[@ColumnRef='code']/SimpleValue=$countryCode]/Value[@ColumnRef='code3']/SimpleValue"/>
		</xsl:element>
	</xsl:template>
	
	
	<!-- Party -->
	<xsl:template mode="residence" match="cac:Country/cbc:IdentificationCode">
		<xsl:variable name="countryCode"><xsl:value-of select="."/></xsl:variable>
		<xsl:element name="ResidenceTypeCode">
			<xsl:choose>
				<xsl:when test='$countryCode ="ES"'>R</xsl:when>
				<xsl:when test='$countryCode ="AT"'>U</xsl:when>
				<xsl:when test='$countryCode="BE"'>U</xsl:when>
				<xsl:when test='$countryCode="BG"'>U</xsl:when>
				<xsl:when test='$countryCode="CY"'>U</xsl:when>
				<xsl:when test='$countryCode="CZ"'>U</xsl:when>
				<xsl:when test='$countryCode="DK"'>U</xsl:when>
				<xsl:when test='$countryCode="EE"'>U</xsl:when>
				<xsl:when test='$countryCode="FI"'>U</xsl:when>
				<xsl:when test='$countryCode="FR"'>U</xsl:when>
				<xsl:when test='$countryCode="DE"'>U</xsl:when>
				<xsl:when test='$countryCode="HE"'>U</xsl:when>
				<xsl:when test='$countryCode="GR"'>U</xsl:when>
				<xsl:when test='$countryCode="GB"'>U</xsl:when>
				<xsl:when test='$countryCode="HU"'>U</xsl:when>
				<xsl:when test='$countryCode="IE"'>U</xsl:when>
				<xsl:when test='$countryCode="IT"'>U</xsl:when>
				<xsl:when test='$countryCode="LV"'>U</xsl:when>
				<xsl:when test='$countryCode="LU"'>U</xsl:when>
				<xsl:when test='$countryCode="MT"'>U</xsl:when>
				<xsl:when test='$countryCode="NL"'>U</xsl:when>
				<xsl:when test='$countryCode="PL"'>U</xsl:when>
				<xsl:when test='$countryCode="PT"'>U</xsl:when>
				<xsl:when test='$countryCode="RO"'>U</xsl:when>
				<xsl:when test='$countryCode="SK"'>U</xsl:when>
				<xsl:when test='$countryCode="SI"'>U</xsl:when>
				<xsl:when test='$countryCode="SE"'>U</xsl:when>
				<xsl:otherwise>E</xsl:otherwise>
			</xsl:choose>
		</xsl:element>	
	</xsl:template>
	<xsl:template match="cbc:CompanyID | cac:PartyIdentification/cbc:ID">
		<xsl:element name="TaxIdentificationNumber">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="Centre" match="cac:PartyIdentification/cbc:ID">
		<xsl:element name="CentreDescription">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:EndpointID">
		<xsl:element name="PhysicalGLN">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>


	<!-- Legal Entity -->
	<xsl:template match="cac:PartyLegalEntity/cbc:RegistrationName">
		<xsl:element name="CorporateName">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:PartyName/cbc:Name">
		<xsl:element name="TradeName">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="individual" match="cac:PartyName/cbc:Name">
		<xsl:element name="Name">
			<xsl:value-of select="."/>
		</xsl:element>
		<xsl:element name="FirstSurname"/>
	</xsl:template>

	<!-- Invoice -->
	<xsl:template match="cbc:ID">
		<xsl:choose>
			<xsl:when test='contains(.,"###")'>
				<xsl:element name="InvoiceNumber">
					<xsl:value-of select='substring-after(.,"###")'/>
				</xsl:element>				
				<xsl:element name="InvoiceSeriesCode">
					<xsl:value-of select='substring-before(.,"###")'/>
				</xsl:element>								
			</xsl:when>
			<xsl:otherwise>
				<xsl:element name="InvoiceNumber">
					<xsl:value-of select="."/>
				</xsl:element>				
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="cbc:InvoiceTypeCode">
		<xsl:element name="InvoiceDocumentType">FC</xsl:element>
		<xsl:choose>
			<xsl:when test=". = '381'"><xsl:element name="InvoiceClass">OR</xsl:element></xsl:when>
			<xsl:otherwise><xsl:element name="InvoiceClass">OO</xsl:element></xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="cbc:CopyIndicator">
		<xsl:choose>
			<xsl:when test='following-sibling::cbc:InvoiceTypeCode = "380"'>
				<xsl:if test=". = 'true'"><xsl:element name="InvoiceClass">CO</xsl:element></xsl:if>
				<xsl:if test=". = 'false'"><xsl:element name="InvoiceClass">OO</xsl:element></xsl:if>
			</xsl:when>
			<xsl:when test='following-sibling::cbc:InvoiceTypeCode = "Comercial"'>
				<xsl:if test=". = 'true'"><xsl:element name="InvoiceClass">CO</xsl:element></xsl:if>
				<xsl:if test=". = 'false'"><xsl:element name="InvoiceClass">OO</xsl:element></xsl:if>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="cbc:IssueDate">
		<xsl:element name="IssueDate">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:TaxPointDate">
		<xsl:element name="OperationDate">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="PlaceOfIssueDescription" match="cbc:CityName">
		<xsl:element name="PlaceOfIssueDescription">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:StartDate">
		<xsl:element name="StartDate">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:EndDate">
		<xsl:element name="EndDate">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:DocumentCurrencyCode">
		<xsl:element name="InvoiceCurrencyCode">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:TaxCurrencyCode">
		<xsl:element name="TaxCurrencyCode">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:TaxExchangeRate/cbc:CalculationRate">
		<xsl:element name="ExchangeRate">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:TaxExchangeRate/cbc:Date">
		<xsl:element name="ExchangeRateDate">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="globalNote" match="cbc:Note">
		<xsl:choose>
			<xsl:when test="starts-with(.,'legal:')">
				<xsl:element name="LegalLiterals">					
				<xsl:element name="LegalReference">
					<xsl:value-of select="substring-after(.,'legal:')"/>
				</xsl:element>
				</xsl:element>
			</xsl:when>
			<xsl:when test="starts-with(.,'additional:')">
				<xsl:element name="AdditionalData">					
				<xsl:element name="InvoiceAdditionalInformation">
					<xsl:value-of select="substring-after(.,'additional:')"/>
				</xsl:element>
				</xsl:element>
			</xsl:when>
			<xsl:otherwise>
				<xsl:element name="LegalLiterals">					
				<xsl:element name="LegalReference">
					<xsl:value-of select="."/>
				</xsl:element>
				</xsl:element>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>	
	
	<!-- Taxes -->
	<xsl:template match="cac:TaxCategory/cac:TaxScheme/cbc:ID">
		<xsl:variable name="taxCode"><xsl:value-of select="."/></xsl:variable>
		<xsl:element name="TaxTypeCode">
			<xsl:choose>
				<xsl:when test='$taxCode ="VAT"'>01</xsl:when>
				<xsl:when test='$taxCode ="IVA"'>01</xsl:when>
				<xsl:when test='$taxCode ="IPSI"'>02</xsl:when>
				<xsl:when test='$taxCode ="IGIC"'>03</xsl:when>
				<xsl:when test='$taxCode ="IRPF"'>04</xsl:when>
				<xsl:when test='$taxCode ="OTHER"'>05</xsl:when>
				<xsl:when test='$taxCode ="ITPAJD"'>06</xsl:when>
				<xsl:when test='$taxCode ="IE"'>07</xsl:when>
				<xsl:when test='$taxCode ="Ra"'>08</xsl:when>
				<xsl:when test='$taxCode ="IGTECM"'>09</xsl:when>
				<xsl:when test='$taxCode ="IECDPCAC"'>10</xsl:when>
				<xsl:when test='$taxCode ="IIIMAB"'>11</xsl:when>
				<xsl:when test='$taxCode ="ICIO"'>12</xsl:when>
				<xsl:when test='$taxCode ="IMVDN"'>13</xsl:when>
				<xsl:when test='$taxCode ="IMSN"'>14</xsl:when>
				<xsl:when test='$taxCode ="IMGSN"'>15</xsl:when>
				<xsl:when test='$taxCode ="IMPN"'>16</xsl:when>
			</xsl:choose>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:Percent">
		<xsl:element name="TaxRate">
			<xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:TaxableAmount">
		<xsl:element name="TotalAmount">
			<xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:TaxAmount">
		<xsl:element name="TotalAmount">
			<xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>

	<!-- Totals -->
	<xsl:template match="cac:LegalMonetaryTotal/cbc:LineExtensionAmount">
		<xsl:element name="TotalGrossAmount">
			<xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount">
		<xsl:element name="TotalGeneralDiscounts">
			<xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:LegalMonetaryTotal/cbc:ChargeTotalAmount">
		<xsl:element name="TotalGeneralSurcharges">
			<xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount">
		<xsl:element name="TotalGrossAmountBeforeTaxes">
			<xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount">
		<xsl:element name="InvoiceTotal">
			<xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:LegalMonetaryTotal/cbc:PrePaidAmount">
		<xsl:element name="TotalPaymentsOnAccount">
			<xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:LegalMonetaryTotal/cbc:PayableAmount">
		<xsl:element name="TotalOutstandingAmount">
			<xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="totalexecutable" match="cac:LegalMonetaryTotal/cbc:PayableAmount">
		<xsl:element name="TotalExecutableAmount">
			<xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>	
	
	<!-- Discounts and Charges -->	
	<xsl:template mode="discount" match="cbc:AllowanceChargeReason">
		<xsl:element name="DiscountReason">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="discount" match="cbc:MultiplierFactorNumeric">
		<xsl:element name="DiscountRate">
			<xsl:value-of select="format-number(., '.0000')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="discount" match="cbc:Amount">
		<xsl:element name="DiscountAmount">
			<xsl:value-of select="format-number(., '0.000000')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="charge" match="cbc:AllowanceChargeReason">
		<xsl:element name="ChargeReason">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="charge" match="cbc:MultiplierFactorNumeric">
		<xsl:element name="ChargeRate">
			<xsl:value-of select="format-number(., '.0000')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="charge" match="cbc:Amount">
		<xsl:element name="ChargeAmount">
			<xsl:value-of select="format-number(., '0.000000')"/>
		</xsl:element>
	</xsl:template>

	<!-- Prepayments -->
	<xsl:template match="cbc:ReceivedDate">
		<xsl:element name="PaymentOnAccountDate">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:PaidAmount">
		<xsl:element name="PaymentOnAccountAmount">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	
	<!-- Invoice Line -->
	<xsl:template match="cbc:InvoicedQuantity">
		<xsl:element name="Quantity">
			<xsl:value-of select="."/>
		</xsl:element>
		<xsl:if test="@unitCode and @unitCodeListID='ES:facturae3'">
			<xsl:element name="UnitOfMeasure">
				<xsl:value-of select="@unitCode"/>
			</xsl:element>
		</xsl:if>
	</xsl:template>
	<xsl:template match="cac:Price/cbc:PriceAmount">
		<xsl:element name="UnitPriceWithoutTax">
			<xsl:value-of select="format-number(., '0.000000')"/>
		</xsl:element>
	</xsl:template>	
	<xsl:template match="cbc:LineExtensionAmount">
		<xsl:element name="TotalCost">
			<xsl:value-of select="format-number(., '0.000000')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="GrossAmount" match="cbc:LineExtensionAmount">
		<xsl:element name="GrossAmount">
			<xsl:value-of select="format-number(., '0.000000')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:ActualDeliveryDate">
		<xsl:element name="TransactionDate">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="InvoiceLine" match="cbc:Note">
		<xsl:element name="AdditionalLineItemInformation">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:Delivery/cbc:ID">
		<xsl:element name="IssuerTransactionReference">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>	
	<xsl:template match="cac:DocumentReference/cbc:ID">
		<xsl:element name="ReceiverTransactionReference">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>	

	
	<!-- Payment Details -->
<!--
	<xsl:template match="cac:PaymentMeans/cbc:PaymentDueDate">
		<xsl:element name="InstallmentDueDate">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
-->
	<xsl:template match="cac:PaymentTerms/cbc:PaymentDueDate">
		<xsl:element name="InstallmentDueDate">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:PaymentTerms/cbc:Amount">
		<xsl:element name="InstallmentAmount">
     <xsl:value-of select="format-number(., '0.00')"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:PaymentMeans/cbc:PaymentMeansCode">
		<xsl:element name="PaymentMeans">
			<!-- Mapping between UNECE4461 and facturae -->
			<xsl:variable name="paymentCode"><xsl:value-of select="."/></xsl:variable>
				<xsl:choose>
					<xsl:when test='$paymentCode="10"'>01</xsl:when>
					<xsl:when test='$paymentCode="20"'>11</xsl:when>
					<xsl:when test='$paymentCode="21"'>17</xsl:when>
					<xsl:when test='$paymentCode="22"'>17</xsl:when>
					<xsl:when test='$paymentCode="23"'>17</xsl:when>
					<xsl:when test='$paymentCode="24"'>08</xsl:when>
					<xsl:when test='$paymentCode="25"'>16</xsl:when>
					<xsl:when test='$paymentCode="31"'>04</xsl:when>
					<xsl:when test='$paymentCode="44"'>05</xsl:when>
					<xsl:when test='$paymentCode="48"'>19</xsl:when>
					<xsl:when test='$paymentCode="49"'>02</xsl:when>
					<xsl:when test='$paymentCode="50"'>15</xsl:when>
					<xsl:when test='$paymentCode="60"'>09</xsl:when>
					<xsl:when test='$paymentCode="61"'>09</xsl:when>
					<xsl:when test='$paymentCode="62"'>09</xsl:when>
					<xsl:when test='$paymentCode="63"'>09</xsl:when>
					<xsl:when test='$paymentCode="64"'>09</xsl:when>
					<xsl:when test='$paymentCode="65"'>09</xsl:when>
					<xsl:when test='$paymentCode="66"'>09</xsl:when>
					<xsl:when test='$paymentCode="67"'>09</xsl:when>
					<xsl:when test='$paymentCode="97"'>15</xsl:when>
					<xsl:when test='$paymentCode="200"'>03</xsl:when>
					<xsl:when test='$paymentCode="201"'>06</xsl:when>
					<xsl:when test='$paymentCode="202"'>07</xsl:when>
					<xsl:when test='$paymentCode="203"'>10</xsl:when>
					<xsl:when test='$paymentCode="204"'>12</xsl:when>
					<xsl:when test='$paymentCode="205"'>18</xsl:when>
					<xsl:when test='$paymentCode="206"'>13</xsl:when>
					<xsl:otherwise>13</xsl:otherwise>
				</xsl:choose>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="PayerFinancialAccount" match="cbc:ID">
		<xsl:choose>
			<xsl:when test='../../cbc:PaymentChannelCode="IBAN"'>
				<xsl:element name="IBAN">
					<xsl:value-of select="."/>
				</xsl:element>				
			</xsl:when>
			<xsl:otherwise>
				<xsl:element name="AccountNumber">
					<xsl:value-of select="."/>
				</xsl:element>				
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template mode="PayeeFinancialAccount" match="cbc:ID">
		<xsl:choose>
			<xsl:when test='../../cbc:PaymentChannelCode="IBAN"'>
				<xsl:element name="IBAN">
					<xsl:value-of select="."/>
				</xsl:element>				
			</xsl:when>
			<xsl:otherwise>
				<xsl:element name="AccountNumber">
					<xsl:value-of select="."/>
				</xsl:element>				
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<xsl:template match="cac:FinancialInstitutionBranch/cac:FinancialInstitution/cbc:ID">
		<xsl:element name="BankCode">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:FinancialInstitutionBranch/cbc:ID">
		<xsl:element name="BranchCode">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cbc:PaymentID">
		<xsl:element name="PaymentReconciliationReference">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:PaymentMeans/cbc:InstructionNote">
		<xsl:element name="CollectionAdditionalInformation">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	
	<!-- Factoring Payment Details -->	
	<xsl:template mode="factoring" match="cbc:PaymentDueDate">
		<xsl:element name="AssignmentDuePaymentDate">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="factoring" match="cbc:PaymentMeansCode">
		<xsl:element name="AssignmentPaymentMeans">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="factoring" match="cac:PaymentMeans/cbc:PaymentID">
		<xsl:element name="PaymentReference">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="factoring" match="cac:PaymentTerms/cbc:Note">
		<xsl:element name="FactoringAssignmentClauses">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
</xsl:stylesheet>
