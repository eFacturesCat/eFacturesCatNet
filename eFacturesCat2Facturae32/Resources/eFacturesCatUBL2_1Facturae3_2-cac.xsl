<?xml version="1.0" encoding="iso-8859-1"?>
<!--
	$Id: InvinetUBL2Facturae3-cac.xsl,v 1.0 2008/02/22 10:15:21 Oriol Bausa $
	
	Conversión de facturas en formato UBL 2.0 a formato Facturae 3.1
	Conversión de Common Aggregate Components
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
	exclude-result-prefixes="ubl cac cbc ccts ext qdt udt ds">

	<xsl:template match="cac:InvoicePeriod">
		<xsl:element name="InvoicingPeriod">
			<xsl:apply-templates select="cbc:StartDate"/>
			<xsl:apply-templates select="cbc:EndDate"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:RequestedDeliveryPeriod">
		<xsl:element name="LineItemPeriod">
			<xsl:apply-templates select="cbc:StartDate"/>
			<xsl:apply-templates select="cbc:EndDate"/>
		</xsl:element>
	</xsl:template>	
	<xsl:template match="cac:Contact">
		<xsl:element name="ContactDetails">
			<xsl:apply-templates select="cbc:Telephone"/>
			<xsl:apply-templates select="cbc:Telefax"/>
			<xsl:apply-templates select="preceding-sibling::cbc:WebsiteURI"/>
			<xsl:apply-templates select="cbc:ElectronicMail"/>
			<xsl:apply-templates select="cbc:Name"/>
			<xsl:apply-templates select="cbc:Note"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:Signature">
		<xsl:element name="InvoiceIssuerType">TE</xsl:element>
		<xsl:element name="ThirdParty">
			<xsl:apply-templates select="cac:SignatoryParty"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:SignatoryParty | cac:PayeeParty">
	<xsl:choose>
		<xsl:when test="cac:PartyLegalEntity">
			<TaxIdentification>
				<PersonTypeCode>J</PersonTypeCode>
					<xsl:apply-templates mode="residence" select="cac:PartyLegalEntity/cac:RegistrationAddress/cac:Country/cbc:IdentificationCode"/>
					<xsl:choose>
						<xsl:when test="cac:PartyTaxScheme/cbc:CompanyID">
							<xsl:apply-templates select="cac:PartyTaxScheme/cbc:CompanyID"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:apply-templates select="cac:PartyLegalEntity/cbc:CompanyID"/>
						</xsl:otherwise>
					</xsl:choose>
			</TaxIdentification>
			<LegalEntity>
				<xsl:apply-templates select="cac:PartyLegalEntity/cbc:RegistrationName"/>
				<xsl:apply-templates select="cac:PartyName/cbc:Name"/>
				<xsl:apply-templates select="cac:PartyLegalEntity/cac:RegistrationAddress"/>
				<xsl:apply-templates select="cac:Contact"/>		
			</LegalEntity>
		</xsl:when>
		<xsl:otherwise>
			<TaxIdentification>
				<PersonTypeCode>F</PersonTypeCode>
					<xsl:apply-templates mode="residence" select="cac:PostalAddress/cac:Country/cbc:IdentificationCode"/>
					<xsl:choose>
						<xsl:when test="cac:PartyTaxScheme/cbc:CompanyID">
							<xsl:apply-templates select="cac:PartyTaxScheme/cbc:CompanyID"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:apply-templates select="cac:PartyIdentification/cbc:ID"/>
						</xsl:otherwise>
					</xsl:choose>
			</TaxIdentification>
			<Individual>
				<xsl:choose>
					<xsl:when test="cac:Person">
						<xsl:apply-templates select="cac:Person/cbc:FirstName"/>
						<xsl:apply-templates select="cac:Person/cbc:FamilyName"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:apply-templates mode="individual" select="cac:PartyName/cbc:Name"/>
					</xsl:otherwise>
				</xsl:choose>
				<xsl:apply-templates select="cac:PostalAddress"/>
				<xsl:apply-templates select="cac:Contact"/>
			</Individual>
		</xsl:otherwise>
	</xsl:choose>				
	</xsl:template>
	<xsl:template match="cac:AccountingSupplierParty/cac:Party | cac:AccountingCustomerParty/cac:Party">
	<xsl:choose>
		<xsl:when test="cac:PartyLegalEntity">
			<TaxIdentification>
				<PersonTypeCode>J</PersonTypeCode>
				<xsl:apply-templates mode="residence" select="cac:PartyLegalEntity/cac:RegistrationAddress/cac:Country/cbc:IdentificationCode"/>
				<xsl:choose>
					<xsl:when test="cac:PartyTaxScheme/cbc:CompanyID">
						<xsl:apply-templates select="cac:PartyTaxScheme/cbc:CompanyID"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:apply-templates select="cac:PartyLegalEntity/cbc:CompanyID"/>
					</xsl:otherwise>
				</xsl:choose>
			</TaxIdentification>
			<xsl:choose>
				<xsl:when test="cac:PostalAddress">
					<AdministrativeCentres>
						<AdministrativeCentre>
							<xsl:apply-templates select="cac:BuyerContact/cbc:Name"/>
							<xsl:apply-templates select="cac:PostalAddress"/>
							<!-- <xsl:apply-templates select="cac:Contact"/> -->
							<xsl:apply-templates select="cbc:EndpointID"/>
							<xsl:apply-templates mode="Centre" select="cac:PartyIdentification/cbc:ID"/>
						</AdministrativeCentre>

					</AdministrativeCentres>
				</xsl:when>
				<xsl:otherwise>
					<xsl:if test="cac:PartyIdentification/cbc:ID">
						<PartyIdentification><xsl:value-of select="cac:PartyIdentification/cbc:ID"/></PartyIdentification>
					</xsl:if>					
				</xsl:otherwise>
			</xsl:choose>
			<LegalEntity>
				<xsl:apply-templates select="cac:PartyLegalEntity/cbc:RegistrationName"/>
				<xsl:apply-templates select="cac:PartyName/cbc:Name"/>
				<xsl:apply-templates select="cac:PartyLegalEntity/cac:RegistrationAddress"/>
				<xsl:apply-templates select="cac:Contact"/>
			</LegalEntity>
		</xsl:when>
	<xsl:otherwise>
		<TaxIdentification>
			<PersonTypeCode>F</PersonTypeCode>
				<xsl:apply-templates mode="residence" select="cac:PostalAddress/cac:Country/cbc:IdentificationCode"/>
				<xsl:choose>
					<xsl:when test="cac:PartyTaxScheme/cbc:CompanyID">
						<xsl:apply-templates select="cac:PartyTaxScheme/cbc:CompanyID"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:apply-templates select="cac:PartyIdentification/cbc:ID"/>
					</xsl:otherwise>
				</xsl:choose>
		</TaxIdentification>
		<Individual>
			<xsl:choose>
				<xsl:when test="cac:Person">
					<xsl:apply-templates select="cac:Person/cbc:FirstName"/>
					<xsl:apply-templates select="cac:Person/cbc:FamilyName"/>
				</xsl:when>
				<xsl:otherwise>
					<xsl:apply-templates mode="individual" select="cac:PartyName/cbc:Name"/>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:apply-templates select="cac:PostalAddress"/>
			<xsl:apply-templates select="cac:Contact"/>
		</Individual>
		</xsl:otherwise>
	</xsl:choose>				
	</xsl:template>
	<xsl:template match="cac:TaxExchangeRate">
	<xsl:element name="ExchangeRateDetails">
		<xsl:apply-templates select="cbc:CalculationRate"/>
		<xsl:apply-templates select="cbc:Date"/>
	</xsl:element>
	</xsl:template>
	<xsl:template match="cac:RegistrationAddress | cac:PostalAddress">
		<xsl:choose>
			<xsl:when test='cac:Country/cbc:IdentificationCode = "ES"'>
				<xsl:element name="AddressInSpain">
					<xsl:choose>
						<xsl:when test='cbc:AddressFormatCode = "StructuredLax"'> 
							<xsl:apply-templates select="cbc:StreetName"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:element name="Address">
								<xsl:value-of select="cbc:StreetName"/>								
								<xsl:for-each select="cac:AddressLine">
									<xsl:value-of select="cbc:Line"/><xsl:text> </xsl:text>
								</xsl:for-each>
							</xsl:element>
						</xsl:otherwise> 
					</xsl:choose>
					<xsl:apply-templates mode="spain" select="cbc:PostalZone"/>
					<xsl:apply-templates select="cbc:CityName"/>
					<xsl:choose>
						<xsl:when test="cbc:CountrySubentity">
							<xsl:apply-templates select="cbc:CountrySubentity"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:apply-templates select="cbc:Region"/>
						</xsl:otherwise>
					</xsl:choose>
					<xsl:apply-templates select="cac:Country/cbc:IdentificationCode"/>
				</xsl:element>
			</xsl:when>
			<xsl:otherwise>
				<xsl:element name="OverseasAddress">
					<xsl:choose>
						<xsl:when test='cbc:AddressFormatCode = "StructuredLax"'> 
							<xsl:apply-templates select="cbc:StreetName"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:element name="Address">
								<xsl:value-of select="cbc:StreetName"/>
								<xsl:for-each select="cac:AddressLine">
									<xsl:value-of select="cbc:Line"/><xsl:text> </xsl:text>
								</xsl:for-each>
							</xsl:element>
						</xsl:otherwise> 
					</xsl:choose>
					<xsl:apply-templates mode="overseas" select="cbc:PostalZone">
						<xsl:with-param name="cityName"><xsl:value-of select="cbc:CityName"/></xsl:with-param>
					</xsl:apply-templates>
					<xsl:choose>
						<xsl:when test="cbc:CountrySubentity">
							<xsl:apply-templates select="cbc:CountrySubentity"/>
						</xsl:when>
						<xsl:when test="cbc:CountrySubentityCode">
							<xsl:apply-templates select="cbc:CountrySubentityCode"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:apply-templates select="cbc:Region"/>
						</xsl:otherwise>
					</xsl:choose>
					<xsl:apply-templates select="cac:Country/cbc:IdentificationCode"/>					
				</xsl:element>
			</xsl:otherwise>
			</xsl:choose> 
	</xsl:template>
	<xsl:template match="cac:Address">
		<xsl:choose>
			<xsl:when test='cac:Country/cbc:IdentificationCode = "ES"'>
				<xsl:element name="BranchInSpainAddress">
					<xsl:choose>
						<xsl:when test='cbc:AddressFormatCode = "StructuredLax"'> 
							<xsl:apply-templates select="cbc:StreetName"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:element name="Address">
								<xsl:for-each select="cac:AddressLine">
									<xsl:value-of select="cbc:Line"/><xsl:text> </xsl:text>
								</xsl:for-each>
							</xsl:element>
						</xsl:otherwise> 
					</xsl:choose>
					<xsl:apply-templates mode="spain" select="cbc:PostalZone"/>
					<xsl:apply-templates select="cbc:CityName"/>
					<xsl:choose>
						<xsl:when test="cbc:CountrySubentity">
							<xsl:apply-templates select="cbc:CountrySubentity"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:apply-templates select="cbc:Region"/>
						</xsl:otherwise>
					</xsl:choose>
					<xsl:apply-templates select="cac:Country/cbc:IdentificationCode"/>
				</xsl:element>
			</xsl:when>
			<xsl:otherwise>
				<xsl:element name="OverseasBranchAddress">
					<xsl:choose>
						<xsl:when test='cbc:AddressFormatCode = "StructuredLax"'> 
							<xsl:apply-templates select="cbc:StreetName"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:element name="Address">
								<xsl:for-each select="cac:AddressLine">
									<xsl:value-of select="cbc:Line"/><xsl:text> </xsl:text>
								</xsl:for-each>
							</xsl:element>
						</xsl:otherwise> 
					</xsl:choose>
					<xsl:apply-templates mode="overseas" select="cbc:PostalZone">
						<xsl:with-param name="cityName"><xsl:value-of select="cbc:CityName"/></xsl:with-param>
					</xsl:apply-templates>
					<xsl:choose>
						<xsl:when test="cbc:CountrySubentity">
							<xsl:apply-templates select="cbc:CountrySubentity"/>
						</xsl:when>
						<xsl:otherwise>
							<xsl:apply-templates select="cbc:Region"/>
						</xsl:otherwise>
					</xsl:choose>
					<xsl:apply-templates select="cac:Country/cbc:IdentificationCode"/>					
				</xsl:element>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>	
	<xsl:template mode="discount" match="cac:AllowanceCharge">
		<xsl:element name="Discount">
			<xsl:apply-templates mode="discount" select="cbc:AllowanceChargeReason"/>
			<xsl:apply-templates mode="discount" select="cbc:MultiplierFactorNumeric"/>
			<xsl:apply-templates mode="discount" select="cbc:Amount"/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="charge" match="cac:AllowanceCharge">
		<xsl:element name="Charge">
			<xsl:apply-templates mode="charge" select="cbc:AllowanceChargeReason"/>
			<xsl:apply-templates mode="charge" select="cbc:MultiplierFactorNumeric"/>
			<xsl:apply-templates mode="charge" select="cbc:Amount"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:PrepaidPayment">
		<xsl:element name="PaymentOnAccount">
			<xsl:apply-templates select="cbc:ReceivedDate"/>
			<xsl:apply-templates select="cbc:PaidAmount"/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:InvoiceLine">
		<xsl:element name="InvoiceLine">
     		<xsl:apply-templates select="./cac:Delivery/cbc:ID"/>
			<xsl:apply-templates select="./cac:DocumentReference/cbc:ID"/>
	    	<xsl:apply-templates mode="sequence" select="cbc:ID"/>
			<xsl:apply-templates select="cac:Item"/>
			<xsl:apply-templates select="cbc:InvoicedQuantity"/>
			<xsl:apply-templates select="cac:Price/cbc:PriceAmount"/>
			<xsl:apply-templates select="cbc:LineExtensionAmount"/>
			<xsl:if test="cac:AllowanceCharge/cbc:ChargeIndicator='false'">								
				<xsl:element name="DiscountsAndRebates">
					<xsl:apply-templates mode="discount" select="./cac:AllowanceCharge[cbc:ChargeIndicator='false']"/>
				</xsl:element>
			</xsl:if>	
			<xsl:if test="cac:AllowanceCharge/cbc:ChargeIndicator='true'">
				<xsl:element name="Charges">
					<xsl:apply-templates mode="charge" select="./cac:AllowanceCharge[cbc:ChargeIndicator='true']" />
				</xsl:element>
			</xsl:if>						
			<xsl:apply-templates mode="GrossAmount" select="cbc:LineExtensionAmount"/>
			<xsl:if test="count(./cac:TaxTotal/cac:TaxSubtotal[cac:TaxCategory/cac:TaxScheme/cbc:ID != '05']) > 0">
				<xsl:element name="TaxesOutputs">
					<xsl:apply-templates mode="withoutTaxAmount" select="./cac:TaxTotal/cac:TaxSubtotal[cac:TaxCategory/cac:TaxScheme/cbc:ID != '05']"/>
				</xsl:element>
			</xsl:if>
			<xsl:if test="count(./cac:TaxTotal/cac:TaxSubtotal[cac:TaxCategory/cac:TaxScheme/cbc:ID = '05']) > 0">
				<xsl:element name="TaxesWithheld">
					<xsl:apply-templates select="./cac:TaxTotal/cac:TaxSubtotal[cac:TaxCategory/cac:TaxScheme/cbc:ID = '05']"/>
				</xsl:element>
			</xsl:if>	
			<xsl:apply-templates select="./cac:Delivery/cac:RequestedDeliveryPeriod"/>
			<xsl:apply-templates select="./cac:Delivery/cbc:ActualDeliveryDate"/>
			<xsl:apply-templates mode="InvoiceLine" select="cbc:Note"/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="sequence" match="cbc:ID">
		<xsl:element name="SequenceNumber">
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:Item">
		<xsl:element name="ItemDescription">
				<xsl:value-of select="cbc:Name"/><xsl:text> </xsl:text><xsl:value-of select="cbc:Description"/>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="withTaxAmount" match="cac:TaxTotal/cac:TaxSubtotal">
		<xsl:element name="Tax">
			<xsl:apply-templates select="cac:TaxCategory/cac:TaxScheme/cbc:ID"/>
			<xsl:apply-templates select="cac:TaxCategory/cbc:Percent"/>
			<xsl:if test="cbc:TaxableAmount">
				<xsl:element name="TaxableBase">
					<xsl:apply-templates select="cbc:TaxableAmount"/>
				</xsl:element>
			</xsl:if>
			<xsl:if test="cbc:TaxAmount">
				<xsl:element name="TaxAmount">
					<xsl:apply-templates select="cbc:TaxAmount"/>
				</xsl:element>
			</xsl:if>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="withoutTaxAmount" match="cac:TaxTotal/cac:TaxSubtotal">
		<xsl:element name="Tax">
			<xsl:apply-templates select="cac:TaxCategory/cac:TaxScheme/cbc:ID"/>
			<xsl:apply-templates select="cac:TaxCategory/cbc:Percent"/>
			<xsl:if test="cbc:TaxableAmount">
				<xsl:element name="TaxableBase">
					<xsl:apply-templates select="cbc:TaxableAmount"/>
				</xsl:element>
			</xsl:if>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:PaymentTerms">
		<xsl:element name="Installment">
			<xsl:apply-templates select="cbc:PaymentDueDate"/>
			<xsl:apply-templates select="cbc:Amount"/>
			<xsl:apply-templates select="../cac:PaymentMeans/cbc:PaymentMeansCode"/>
			<xsl:apply-templates select="../cac:PaymentMeans/cac:PayerFinancialAccount"/>
			<xsl:apply-templates select="../cac:PaymentMeans/cac:PayeeFinancialAccount"/>
			<xsl:apply-templates select="../cac:PaymentMeans/cbc:InstructionNote"/>
		</xsl:element>
	</xsl:template>
<!-- Old PaymentMeans	
	<xsl:template match="cac:PaymentMeans">
		<xsl:element name="PaymentDetails">
			<xsl:for-each select=".">
				<xsl:element name="Installment">
					<xsl:apply-templates select="cbc:PaymentDueDate"/>
					<xsl:apply-templates select="following-sibling::cac:PaymentTerms/cbc:Amount"/>
					<xsl:apply-templates select="cbc:PaymentMeansCode"/>
					<xsl:apply-templates select="cac:PayerFinancialAccount"/>
					<xsl:apply-templates select="cac:PayeeFinancialAccount"/>
					<xsl:apply-templates select="cbc:PaymentID"/>
					<xsl:apply-templates select="cbc:InstructionNote"/>
				</xsl:element>
			</xsl:for-each>
		</xsl:element>
	</xsl:template>
	<xsl:template mode="factoring" match="cac:PaymentMeans">
		<xsl:element name="PaymentDetails">			
			<xsl:apply-templates mode="factoring" select="cbc:PaymentDueDate"/>	
			<xsl:apply-templates mode="factoring" select="cbc:PaymentMeansCode"/>
			<xsl:apply-templates select="cac:PayeeFinancialAccount/cbc:ID"/>
			<xsl:apply-templates mode="factoring" select="cbc:PaymentID"/>
		</xsl:element>
	</xsl:template>
  -->
	<xsl:template match="cac:PayeeFinancialAccount">
		<xsl:element name="AccountToBeCredited">
			<xsl:apply-templates mode="PayeeFinancialAccount" select="cbc:ID"/>
			<xsl:apply-templates select="cac:FinancialInstitutionBranch/cac:FinancialInstitution/cbc:ID"/>
			<xsl:apply-templates select="cac:FinancialInstitutionBranch/cbc:ID"/>
			<xsl:if test="count(cac:FinancialInstitutionBranch/cac:FinancialInstitution/cac:Address) > 0">
				<xsl:apply-templates select="cac:FinancialInstitutionBranch/cac:FinancialInstitution/cac:Address"/>
			</xsl:if>
		</xsl:element>
	</xsl:template>
	<xsl:template match="cac:PayerFinancialAccount">
		<xsl:element name="AccountToBeDebited">
			<xsl:apply-templates mode="PayerFinancialAccount" select="cbc:ID"/>
			<xsl:apply-templates select="cac:FinancialInstitutionBranch/cac:FinancialInstitution/cbc:ID"/>
			<xsl:apply-templates select="cac:FinancialInstitutionBranch/cbc:ID"/>
			<xsl:if test="count(cac:FinancialInstitutionBranch/cac:FinancialInstitution/cac:Address) > 0">
				<xsl:apply-templates select="cac:FinancialInstitutionBranch/cac:FinancialInstitution/cac:Address"/>
			</xsl:if>
			</xsl:element>
	</xsl:template>
</xsl:stylesheet>
