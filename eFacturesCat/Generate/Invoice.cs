using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace eFacturesCat.Generate
{
    public class Invoice
    {

        //TODO: Review other currencies than euro
        //TODO: Rectificativas
        //TODO: IRPF
        //TODO: Discounts

        public InvoiceType inv { get; private set; }

        private InvoiceTypeCodeType invoiceTypeCode;
        private String invoiceCurrencyCode;
        private String invoiceLanguage;
        private int invoiceLineRoundingDecimalPlaces;
        private int invoiceTotalsRoundingDecimalPlaces;
        private List<InvoiceLineType> invoiceLinesList = new List<InvoiceLineType>();
        private List<PaymentTermsType> paymentTermsList = new List<PaymentTermsType>();

        private const string Const_UBLVERSION = "2.1";
        private const string Const_INVOICENUMBERSEPARATOR = "###";
        private const string Const_LISTID_INVOICETYPE = "UN/ECE 1001 Subset";
        private const string Const_LISTID_CURRENCY = "ISO 4217 Alpha";
        private const string Const_LISTID_COUNTRY = "ISO3166-1";
        private const string Const_LISTID_PAYMENTMEANS = "UN/ECE 4461";
        private const string Const_SCHEMEID_VAT = "UN/ECE 5153";
        private const string Const_SCHEMEID_VAT_CAT = "UN/ECE 5305";
        private const string Const_SPANISH_SCHEMEID_VAT = "NIF";
        private const string Const_AGENCYID_UNECE = "6";
        private const string Const_AGENCYID_AEAT = "AEAT";
        private const string Const_NORMALINVOICECODE = "380";
        private const string Const_CREDITNOTECODE = "381";
        private const string Const_IBANCODE = "IBAN";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="InvoiceCurrencyCode">ISO 4217 Alpha Code - By now only EUR</param>
        /// <param name="InvoiceLanguage">ISO 639-1:2002 Alpha-2 Code Language: es, ca, en ...</param>
        /// <param name="LineRoundingDecimalPlaces"></param>
        /// <param name="TotalsRoundingDecimalPlaces"></param>
        /// <param name="CreditNote"></param>
        public Invoice(String currencyCode, String language, int lineRoundingDecimalPlaces, int totalsRoundingDecimalPlaces, bool isCreditNote)
        {
            inv = new InvoiceType();            
            invoiceCurrencyCode = currencyCode;
            invoiceLanguage = language;
            invoiceLineRoundingDecimalPlaces = lineRoundingDecimalPlaces;
            invoiceTotalsRoundingDecimalPlaces = totalsRoundingDecimalPlaces;
            invoiceTypeCode = new InvoiceTypeCodeType();
            inv.InvoiceTypeCode = invoiceTypeCode;
            UBLVersionIDType uv = new UBLVersionIDType();
            uv.Value = Const_UBLVERSION;
            inv.UBLVersionID = uv;
            invoiceTypeCode.listID = Const_LISTID_INVOICETYPE;
            invoiceTypeCode.listAgencyID = Const_AGENCYID_UNECE;
            invoiceTypeCode.Value = Const_NORMALINVOICECODE;
            if (isCreditNote)
                invoiceTypeCode.Value = Const_CREDITNOTECODE;
            else
                invoiceTypeCode.Value = Const_NORMALINVOICECODE;
        }


        /// <summary>
        /// Constructor from XML eFacturesCatInvoice
        /// </summary>
        /// <param name="fileName"></param>
        public Invoice(string fileName)
        {
            StreamReader stream = new StreamReader(fileName);
            XmlSerializer mySerializer = new XmlSerializer(typeof(InvoiceType));
            inv = (InvoiceType)mySerializer.Deserialize(stream);
            //TODO: get currency, language and rounding decimal places
        }

        /// <summary>
        /// Set InvoiceNumber and Series Code (optional) for Invoice
        /// </summary>
        /// <param name="SeriesCode">Could be null</param>
        /// <param name="InvoiceNumber"></param>
        /// <param name="dateIssue"></param>
        public void setInvoiceHeader(String seriesCode, String invoiceNumber, DateTime dateIssue)
        {
            String tmpInvoiceNumber;
            if (seriesCode!=null) tmpInvoiceNumber = seriesCode + Const_INVOICENUMBERSEPARATOR + invoiceNumber;
            else tmpInvoiceNumber = invoiceNumber;
            IDType InvoiceID = new IDType();
            InvoiceID.Value = tmpInvoiceNumber;
            inv.ID = InvoiceID;

            IssueDateType issueDate = new IssueDateType();
            inv.IssueDate = issueDate;
            issueDate.Value = dateIssue;

            DocumentCurrencyCodeType docCurrencyCode = new DocumentCurrencyCodeType();
            inv.DocumentCurrencyCode = docCurrencyCode;
            docCurrencyCode.listID = Const_LISTID_CURRENCY;
            docCurrencyCode.listAgencyID = Const_AGENCYID_UNECE;
            docCurrencyCode.Value = invoiceCurrencyCode;

        }

        /// <summary>
        /// If creditNote set Corrective node is mandatory
        /// </summary>
        /// <param name="invoiceNumberToBeCorrected"></param>
        /// <param name="reasonCode"></param>
        /// <param name="reasonDesc"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="correctionMethodCode"></param>
        /// <param name="correctionMethodDesc"></param>
        public void setCorrectiveNode(String invoiceNumberToBeCorrected, ReasonCodeType reasonCode, ReasonDescriptionType reasonDesc, DateTime startDate, DateTime endDate, 
            CorrectionMethodType correctionMethodCode, CorrectionMethodDescriptionType correctionMethodDesc)
        {
            inv.Corrective = new CorrectiveType();
            inv.Corrective.InvoiceNumber = invoiceNumberToBeCorrected;
            inv.Corrective.ReasonCode = reasonCode;
            inv.Corrective.ReasonDescription = reasonDesc;
            inv.Corrective.TaxPeriod = new PeriodDates();
            inv.Corrective.TaxPeriod.StartDate = startDate;
            inv.Corrective.TaxPeriod.EndDate = endDate;
            inv.Corrective.CorrectionMethod = correctionMethodCode;
            inv.Corrective.CorrectionMethodDescription = correctionMethodDesc;           
        }

        /// <summary>
        /// Set the seller legal party of invoice
        /// </summary>
        /// <param name="VATiD">NIF</param>
        /// <param name="entityName"></param>
        /// <param name="entityAddress"></param>
        /// <param name="entityTown"></param>
        /// <param name="entityPostalCode"></param>
        /// <param name="entityRegion"></param>
        /// <param name="entityCountryCode">ISO3166-1 i.e. ES</param>
        /// <param name="entityEmail"></param>
        /// <param name="partyID"></param>
        /// <returns>the party</returns>
        public PartyType setSellerParty(String VATiD, String entityName, String entityAddress, String entityTown,
            String entityPostalCode, String entityRegion, String entityCountryCode, String entityEmail, String partyID)
        {
            SupplierPartyType sp = new SupplierPartyType();
            inv.AccountingSupplierParty = sp;
            sp.Party = new PartyType();
            sp.Party.PartyTaxScheme = createPartyTaxScheme(VATiD);
            sp.Party.PartyLegalEntity = createPartyLegalEntity(entityName, entityAddress, entityTown, entityPostalCode, entityRegion, entityCountryCode);

            if (!String.IsNullOrEmpty(partyID))
            {
                sp.Party.PartyIdentification = new PartyIdentificationType[1];
                sp.Party.PartyIdentification[0] = new PartyIdentificationType();
                sp.Party.PartyIdentification[0].ID = new IDType();
                sp.Party.PartyIdentification[0].ID.Value = partyID;
            }
            if (!String.IsNullOrEmpty(entityEmail)) addContact(sp.Party, entityEmail, null);
            
            return sp.Party;
        }

        /// <summary>
        /// Set the seller individual person party of the invoice
        /// </summary>
        /// <param name="VATiD">NIF</param>
        /// <param name="firstName"></param>
        /// <param name="familyName"></param>
        /// <param name="entityAddress"></param>
        /// <param name="entityTown"></param>
        /// <param name="entityPostalCode"></param>
        /// <param name="entityRegion"></param>
        /// <param name="entityCountryCode">ISO3166-1 i.e. ES</param>
        /// <param name="entityEmail"></param>
        /// <param name="partyID"></param>
        /// <returns></returns>
        public PartyType setSellerPersonParty(String VATiD, String firstName, String familyName, String entityAddress, String entityTown,
            String entityPostalCode, String entityRegion, String entityCountryCode, String entityEmail, String partyID)
        {
            SupplierPartyType sp = new SupplierPartyType();
            inv.AccountingSupplierParty = sp;
            sp.Party = new PartyType();
            sp.Party.PartyTaxScheme = createPartyTaxScheme(VATiD);
            sp.Party.PostalAddress = new AddressType();
            createAddress(sp.Party.PostalAddress, entityAddress, entityTown, entityPostalCode, entityRegion, entityCountryCode);
            sp.Party.Person = new PersonType[1];
            sp.Party.Person[0] = new PersonType();
            sp.Party.Person[0].FirstName = new FirstNameType();
            sp.Party.Person[0].FirstName.Value = firstName;
            sp.Party.Person[0].FamilyName = new FamilyNameType();
            sp.Party.Person[0].FamilyName.Value = familyName;

            if (!String.IsNullOrEmpty(partyID))
            {
                sp.Party.PartyIdentification = new PartyIdentificationType[1];
                sp.Party.PartyIdentification[0] = new PartyIdentificationType();
                sp.Party.PartyIdentification[0].ID = new IDType();
                sp.Party.PartyIdentification[0].ID.Value = partyID;
            }
            if (!String.IsNullOrEmpty(entityEmail)) addContact(sp.Party, entityEmail, null);

            return sp.Party;
        }

        /// <summary>
        /// Set the buyer individual party of the invoice 
        /// </summary>
        /// <param name="VATiD">NIF</param>
        /// <param name="firstName"></param>
        /// <param name="familyName"></param>
        /// <param name="entityAddress"></param>
        /// <param name="entityTown"></param>
        /// <param name="entityPostalCode"></param>
        /// <param name="entityRegion"></param>
        /// <param name="entityCountryCode">ISO3166-1 i.e. ES</param>
        /// <param name="entityEmail"></param>
        /// <param name="partyID"></param>
        /// <param name="moreInfo"></param>
        /// <returns></returns>
        public PartyType setBuyerPersonParty(String VATiD, String firstName, String familyName, String entityAddress, String entityTown,
            String entityPostalCode, String entityRegion, String entityCountryCode, String entityEmail, String partyID, String moreInfo)
        {
            CustomerPartyType cp = new CustomerPartyType();
            inv.AccountingCustomerParty = cp;
            cp.Party = new PartyType();

            if (!String.IsNullOrEmpty(invoiceLanguage))
            {
                cp.Party.Language = new LanguageType();
                cp.Party.Language.ID = new IDType();
                cp.Party.Language.ID.Value = invoiceLanguage;
            }

            cp.Party.PartyTaxScheme = createPartyTaxScheme(VATiD);
            cp.Party.PostalAddress = new AddressType();
            createAddress(cp.Party.PostalAddress, entityAddress, entityTown, entityPostalCode, entityRegion, entityCountryCode);
            cp.Party.Person = new PersonType[1];
            cp.Party.Person[0] = new PersonType();
            cp.Party.Person[0].FirstName = new FirstNameType();
            cp.Party.Person[0].FirstName.Value = firstName;
            cp.Party.Person[0].FamilyName = new FamilyNameType();
            cp.Party.Person[0].FamilyName.Value = familyName;

            if (!String.IsNullOrEmpty(partyID))
            {
                cp.Party.PartyIdentification = new PartyIdentificationType[1];
                cp.Party.PartyIdentification[0] = new PartyIdentificationType();
                cp.Party.PartyIdentification[0].ID = new IDType();
                cp.Party.PartyIdentification[0].ID.Value = partyID;
            }

            if (!String.IsNullOrEmpty(entityEmail) || !String.IsNullOrEmpty(moreInfo)) addContact(cp.Party, entityEmail, moreInfo);

            return cp.Party;
        }
        
        /// <summary>
        /// Set the buyer legal entity of the invoice
        /// </summary>
        /// <param name="VATiD">NIF</param>
        /// <param name="entityName"></param>
        /// <param name="entityAddress"></param>
        /// <param name="entityTown"></param>
        /// <param name="entityPostalCode"></param>
        /// <param name="entityRegion"></param>
        /// <param name="entityCountryCode">ISO3166-1 i.e. ES</param>
        /// <param name="entityEmail"></param>
        /// <param name="partyID"></param>
        /// <param name="moreInfo"></param>
        /// <returns></returns>
        public PartyType setBuyerParty(String VATiD, String entityName, String entityAddress, String entityTown,
            String entityPostalCode, String entityRegion, String entityCountryCode, String entityEmail, String partyID, String moreInfo)
        {
            CustomerPartyType cp = new CustomerPartyType();
            inv.AccountingCustomerParty = cp;
            cp.Party = new PartyType();
            if (!String.IsNullOrEmpty(invoiceLanguage))
            {
                cp.Party.Language = new LanguageType();
                cp.Party.Language.ID = new IDType();
                cp.Party.Language.ID.Value = invoiceLanguage;
            }

            cp.Party.PartyTaxScheme = createPartyTaxScheme(VATiD);
            cp.Party.PartyLegalEntity = createPartyLegalEntity(entityName, entityAddress, entityTown, entityPostalCode, entityRegion, entityCountryCode);

            if (!String.IsNullOrEmpty(partyID))
            {
                cp.Party.PartyIdentification = new PartyIdentificationType[1];
                cp.Party.PartyIdentification[0] = new PartyIdentificationType();
                cp.Party.PartyIdentification[0].ID = new IDType();
                cp.Party.PartyIdentification[0].ID.Value = partyID;
            }

            if (!String.IsNullOrEmpty(entityEmail) || !String.IsNullOrEmpty(moreInfo)) addContact(cp.Party, entityEmail, moreInfo);

            return cp.Party;
        }

        /// <summary>
        /// add line to invoice lines list (when all lines are added, needs a call to closeLines method)
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="invoicedQuantity"></param>
        /// <param name="price"></param>
        /// <param name="VATcategory">UN/ECE 5305 - S for Standard</param>
        /// <param name="VATpercent"></param>
        /// <param name="moreInfo"></param>
        /// <returns></returns>
        public InvoiceLineType addInvoiceLine(String itemName, double invoicedQuantity, double price, String VATcategory, double VATpercent, String moreInfo)
        {
            InvoiceLineType il = new InvoiceLineType();
            invoiceLinesList.Add(il);
            il.ID = new IDType();
            il.ID.Value = invoiceLinesList.Count.ToString("0000");
            if (!String.IsNullOrEmpty(moreInfo))
            {
                il.Note = new NoteType[1];
                il.Note[0] = new NoteType();
                il.Note[0].Value = moreInfo;
            }

            il.InvoicedQuantity = new InvoicedQuantityType();
            il.InvoicedQuantity.Value = Convert.ToDecimal(invoicedQuantity);

            il.Item = new ItemType();
            il.Item.Name = new NameType1();
            il.Item.Name.Value = itemName;

            il.Price = new PriceType();
            il.Price.PriceAmount = new PriceAmountType();
            il.Price.PriceAmount.currencyID = invoiceCurrencyCode;
            il.Price.PriceAmount.Value = Convert.ToDecimal(price);
            il.Price.BaseQuantity = new BaseQuantityType();
            il.Price.BaseQuantity.Value = 1;

            il.LineExtensionAmount = new LineExtensionAmountType();
            il.LineExtensionAmount.currencyID = invoiceCurrencyCode;
            il.LineExtensionAmount.Value = Decimal.Round(Convert.ToDecimal(invoicedQuantity * price), invoiceLineRoundingDecimalPlaces);

            il.TaxTotal = new TaxTotalType[1];
            il.TaxTotal[0] = new TaxTotalType();
            il.TaxTotal[0].TaxAmount = new TaxAmountType();
            il.TaxTotal[0].TaxAmount.currencyID = invoiceCurrencyCode;

            // If there are global discounts aply in taxes

            double globalDiscount = 0;
            if (inv.AllowanceCharge != null)
            {
                // SCD - 2.2.0.3 Calculate final globaldiscounts at calculateTotals
                //globalDiscount = Convert.ToDouble(il.LineExtensionAmount.Value * inv.AllowanceCharge[0].MultiplierFactorNumeric.Value / 100);
                // SCD - 2.2.0.1 - Rounding global discount as invoiceTotalsRoundingDecimalPlaces
                globalDiscount = Convert.ToDouble(
                    Decimal.Round(il.LineExtensionAmount.Value * inv.AllowanceCharge[0].MultiplierFactorNumeric.Value / 100, invoiceTotalsRoundingDecimalPlaces));
                inv.AllowanceCharge[0].Amount.Value += Convert.ToDecimal(globalDiscount);
            }


            il.TaxTotal[0].TaxAmount.Value = Decimal.Round(Convert.ToDecimal(((invoicedQuantity * price - globalDiscount) * VATpercent / 100)), invoiceLineRoundingDecimalPlaces);
            il.TaxTotal[0].TaxSubtotal = new TaxSubtotalType[1];
            il.TaxTotal[0].TaxSubtotal[0] =
                createTaxSubtotal(il.LineExtensionAmount.Value - Convert.ToDecimal(globalDiscount), il.TaxTotal[0].TaxAmount.Value, VATcategory, Convert.ToDecimal(VATpercent), "VAT");
            return il;                        
        }

        /// <summary>
        /// Create a TaxSubtotalNode
        /// </summary>
        /// <param name="taxableAmount"></param>
        /// <param name="taxAmount"></param>
        /// <param name="VATcategory"></param>
        /// <param name="VATpercent"></param>
        /// <param name="taxSchemaID"></param>
        /// <returns></returns>
        public TaxSubtotalType createTaxSubtotal(decimal taxableAmount, decimal taxAmount, String VATcategory, decimal VATpercent, String taxSchemaID)
        {
            TaxSubtotalType ts = new TaxSubtotalType();
            ts.TaxableAmount = new TaxableAmountType();
            ts.TaxableAmount.currencyID = invoiceCurrencyCode;
            ts.TaxableAmount.Value = taxableAmount;
            ts.TaxAmount = new TaxAmountType();
            ts.TaxAmount.currencyID = invoiceCurrencyCode;
            ts.TaxAmount.Value = taxAmount;
            ts.TaxCategory = new TaxCategoryType();
            ts.TaxCategory.ID = new IDType();
            ts.TaxCategory.ID.schemeID = Const_SCHEMEID_VAT_CAT;
            ts.TaxCategory.ID.schemeAgencyID = Const_AGENCYID_UNECE;
            ts.TaxCategory.ID.Value = VATcategory;
            ts.TaxCategory.Percent = new PercentType1();
            ts.TaxCategory.Percent.Value = VATpercent;
            ts.TaxCategory.TaxScheme = new TaxSchemeType();
            ts.TaxCategory.TaxScheme = new TaxSchemeType();
            ts.TaxCategory.TaxScheme.ID = new IDType();
            ts.TaxCategory.TaxScheme.ID.schemeID = Const_SCHEMEID_VAT;
            ts.TaxCategory.TaxScheme.ID.schemeAgencyID = Const_AGENCYID_UNECE;
            ts.TaxCategory.TaxScheme.ID.Value = taxSchemaID;
            return ts;
        }

        /// <summary>
        /// Insert array of line in invoice
        /// </summary>
        public void closeLines()
        {
            inv.InvoiceLine = invoiceLinesList.ToArray();
        }

        /// <summary>
        /// If there are, sets a global discount
        /// </summary>
        /// <param name="percent"></param>
        /// <param name="discountDescription"></param>
        public void setGlobalDiscount(decimal percent, string discountDescription)
        {
            inv.AllowanceCharge = new AllowanceChargeType[1];
            inv.AllowanceCharge[0] = new AllowanceChargeType();
            inv.AllowanceCharge[0].ChargeIndicator = new ChargeIndicatorType();
            inv.AllowanceCharge[0].ChargeIndicator.Value = false;
            inv.AllowanceCharge[0].MultiplierFactorNumeric = new MultiplierFactorNumericType();
            inv.AllowanceCharge[0].MultiplierFactorNumeric.Value = percent;
            inv.AllowanceCharge[0].AllowanceChargeReason = new AllowanceChargeReasonType();
            inv.AllowanceCharge[0].AllowanceChargeReason.Value = discountDescription;
            inv.AllowanceCharge[0].Amount = new AmountType2();
            inv.AllowanceCharge[0].Amount.currencyID = invoiceCurrencyCode;
            inv.AllowanceCharge[0].Amount.Value = 0;
        }
        /// <summary>
        /// CalculateTotal from InvoiceLines
        /// </summary>
        public void calculateTotals()
        {
            inv.TaxTotal = new TaxTotalType[1];
            inv.TaxTotal[0] = new TaxTotalType();
            inv.TaxTotal[0].TaxAmount = new TaxAmountType();
            inv.TaxTotal[0].TaxAmount.currencyID = invoiceCurrencyCode;
            inv.TaxTotal[0].TaxAmount.Value = 0;

            inv.LegalMonetaryTotal = new MonetaryTotalType();

            // Global Discounts
            Decimal globalDiscount = 0;
            //if (inv.AllowanceCharge != null)
            //{
            //    globalDiscount = inv.AllowanceCharge[0].Amount.Value;
            //    inv.LegalMonetaryTotal.AllowanceTotalAmount = new AllowanceTotalAmountType();
            //    inv.LegalMonetaryTotal.AllowanceTotalAmount.currencyID = invoiceCurrencyCode;
            //    inv.LegalMonetaryTotal.AllowanceTotalAmount.Value = globalDiscount;
            //}


            inv.LegalMonetaryTotal.LineExtensionAmount = new LineExtensionAmountType();
            inv.LegalMonetaryTotal.LineExtensionAmount.currencyID = invoiceCurrencyCode;
            inv.LegalMonetaryTotal.LineExtensionAmount.Value = 0;
            inv.LegalMonetaryTotal.TaxExclusiveAmount = new TaxExclusiveAmountType();
            inv.LegalMonetaryTotal.TaxExclusiveAmount.currencyID = invoiceCurrencyCode; 
            //inv.LegalMonetaryTotal.TaxExclusiveAmount.Value = -globalDiscount;
            inv.LegalMonetaryTotal.TaxExclusiveAmount.Value = 0;
            //
            inv.LegalMonetaryTotal.TaxInclusiveAmount = new TaxInclusiveAmountType();
            inv.LegalMonetaryTotal.TaxInclusiveAmount.currencyID = invoiceCurrencyCode; 
            //inv.LegalMonetaryTotal.TaxInclusiveAmount.Value = -globalDiscount;
            inv.LegalMonetaryTotal.TaxInclusiveAmount.Value = 0;
            //
            inv.LegalMonetaryTotal.PayableAmount = new PayableAmountType();
            inv.LegalMonetaryTotal.PayableAmount.currencyID = invoiceCurrencyCode; 
            //inv.LegalMonetaryTotal.PayableAmount.Value = -globalDiscount;
            inv.LegalMonetaryTotal.PayableAmount.Value = 0; ;

            List<TaxSubtotalType> taxSubtotalList = new List<TaxSubtotalType>();
            foreach (InvoiceLineType il in invoiceLinesList)
            {
                addTaxSubtotalTotal(taxSubtotalList, il);
                //inv.TaxTotal[0].TaxAmount.Value += il.TaxTotal[0].TaxAmount.Value;
                inv.LegalMonetaryTotal.LineExtensionAmount.Value += il.LineExtensionAmount.Value;
                inv.LegalMonetaryTotal.TaxExclusiveAmount.Value += il.LineExtensionAmount.Value;
                //inv.LegalMonetaryTotal.TaxInclusiveAmount.Value += il.LineExtensionAmount.Value + il.TaxTotal[0].TaxAmount.Value;
                //inv.LegalMonetaryTotal.PayableAmount.Value += il.LineExtensionAmount.Value + il.TaxTotal[0].TaxAmount.Value;
                inv.LegalMonetaryTotal.TaxInclusiveAmount.Value += il.LineExtensionAmount.Value;
                inv.LegalMonetaryTotal.PayableAmount.Value += il.LineExtensionAmount.Value;
            }

            // Calculate GlobalDiscount
            if (inv.AllowanceCharge != null)
            {
                //globalDiscount = inv.AllowanceCharge[0].Amount.Value;
                globalDiscount = Decimal.Round(inv.LegalMonetaryTotal.LineExtensionAmount.Value * inv.AllowanceCharge[0].MultiplierFactorNumeric.Value / 100, invoiceTotalsRoundingDecimalPlaces);
                inv.LegalMonetaryTotal.AllowanceTotalAmount = new AllowanceTotalAmountType();
                inv.LegalMonetaryTotal.AllowanceTotalAmount.currencyID = invoiceCurrencyCode;
                inv.LegalMonetaryTotal.AllowanceTotalAmount.Value = globalDiscount;
                inv.AllowanceCharge[0].Amount.Value = globalDiscount;
                inv.LegalMonetaryTotal.TaxExclusiveAmount.Value -= globalDiscount;
                inv.LegalMonetaryTotal.TaxInclusiveAmount.Value -= globalDiscount;
                inv.LegalMonetaryTotal.PayableAmount.Value -= globalDiscount;
            }
            

            
            // Acumule Tax
            foreach (TaxSubtotalType tax in taxSubtotalList)
            {
                inv.TaxTotal[0].TaxAmount.Value += tax.TaxAmount.Value;
                inv.LegalMonetaryTotal.TaxInclusiveAmount.Value += tax.TaxAmount.Value;
                inv.LegalMonetaryTotal.PayableAmount.Value += tax.TaxAmount.Value;
            }
            inv.TaxTotal[0].TaxSubtotal = taxSubtotalList.ToArray();
        }

        /// <summary>
        /// Set the payment mean of the invoice
        /// </summary>
        /// <param name="paymentMeansCode">From UN/ECE 4461</param>
        /// <param name="bankAccount"></param>
        /// <param name="isIBANcode">true if bankAccount is IBAN format</param>
        /// <param name="note">detail description of payment means</param>
        public void setPaymentMeans(String paymentMeansCode, String bankAccount, bool isIBANcode, string note)
        {
            inv.PaymentMeans = new PaymentMeansType[1];
            inv.PaymentMeans[0] = new PaymentMeansType();
            inv.PaymentMeans[0].PaymentMeansCode = new PaymentMeansCodeType();
            inv.PaymentMeans[0].PaymentMeansCode.listID = Const_LISTID_PAYMENTMEANS;
            inv.PaymentMeans[0].PaymentMeansCode.Value = paymentMeansCode;
            if (!String.IsNullOrEmpty(note))
            {
                inv.PaymentMeans[0].InstructionNote = new InstructionNoteType[1];
                inv.PaymentMeans[0].InstructionNote[0] = new InstructionNoteType();
                inv.PaymentMeans[0].InstructionNote[0].Value = note;

            }
            if (!String.IsNullOrEmpty(bankAccount))
            {
                if (isIBANcode)
                {
                    inv.PaymentMeans[0].PaymentChannelCode = new PaymentChannelCodeType();
                    inv.PaymentMeans[0].PaymentChannelCode.Value = Const_IBANCODE;
                }
                FinancialAccountType financialAccount = new FinancialAccountType();
                financialAccount.ID = new IDType();
                financialAccount.ID.Value = bankAccount;

                // Choose between payee and payer
                if (isBankTransfer(paymentMeansCode))
                {
                    inv.PaymentMeans[0].PayeeFinancialAccount = financialAccount;
                }
                else
                {
                    inv.PaymentMeans[0].PayerFinancialAccount = financialAccount;
                }
            }
        }

        /// <summary>
        /// add paymentterms (at end must be closed callint closePaymentTerms method)
        /// </summary>
        /// <param name="isPercent">true if next param is a percent of invoice total. Otherwise is an amount</param>
        /// <param name="amountOrPercent"></param>
        /// <param name="dueDate"></param>
        /// <param name="moreInfo"></param>
        /// <returns></returns>
        public PaymentTermsType addPaymentTerm(bool isPercent, double amountOrPercent, DateTime dueDate, String moreInfo)
        {
            PaymentTermsType pt = new PaymentTermsType();
            pt.PaymentDueDate = new PaymentDueDateType();
            pt.PaymentDueDate.Value = dueDate;
            if (!String.IsNullOrEmpty(moreInfo))
            {
                pt.Note = new NoteType[1];
                pt.Note[0] = new NoteType();
                pt.Note[0].Value = moreInfo;
            }
            pt.Amount = new AmountType2();
            pt.Amount.currencyID = invoiceCurrencyCode;
            if (isPercent)
            {
                double amount = Convert.ToDouble(inv.LegalMonetaryTotal.PayableAmount.Value) * amountOrPercent / 100;
                pt.Amount.Value = Decimal.Round(Convert.ToDecimal(amount), invoiceTotalsRoundingDecimalPlaces);
            }
            else
            {
                pt.Amount.Value = Convert.ToDecimal(amountOrPercent);
            }
            paymentTermsList.Add(pt);
            return pt;
        }

        /// <summary>
        /// insert array of payment terms to invoice
        /// </summary>
        public void closePaymentTerms()
        {
            inv.PaymentTerms = paymentTermsList.ToArray();
        }

        /// <summary>
        /// set additional information for invoice
        /// </summary>
        /// <param name="moreInfo"></param>
        public void setAdditionalInvoiceInfo(String moreInfo)
        {
            inv.Note = new NoteType[1];
            inv.Note[0] = new NoteType();
            inv.Note[0].Value = moreInfo;
        }

        /// <summary>
        /// add additional information for invoice
        /// </summary>
        /// <param name="moreInfo"></param>
        public void addAdditionalInvoiceInfo(String moreInfo)
        {
            int numnotes;
            NoteType note = new NoteType();
            note.Value = moreInfo;
            if (inv.Note!=null) numnotes = inv.Note.Length + 1;
            else numnotes = 1;
            NoteType[] notes = new NoteType[numnotes];
            for (int i = 0; i < numnotes - 1; i++) notes[i] = inv.Note[i];
            notes[numnotes - 1] = note;
            inv.Note = notes;
        }

        private bool isBankTransfer(String paymentMeansCode)
        {
            if (paymentMeansCode == "30") return true;
            if (paymentMeansCode == "31") return true;
            return false;
        }

        private void addTaxSubtotalTotal(List<TaxSubtotalType> taxSubtotalList, InvoiceLineType il)
        {
            foreach (TaxSubtotalType taxSubtotal in taxSubtotalList)
            {
                //TODO: Review acum conditions
                if (taxSubtotal.TaxCategory.ID.Value.Equals(il.TaxTotal[0].TaxSubtotal[0].TaxCategory.ID.Value) &&
                    taxSubtotal.TaxCategory.Percent.Value.Equals(il.TaxTotal[0].TaxSubtotal[0].TaxCategory.Percent.Value) &&
                    taxSubtotal.TaxCategory.TaxScheme.ID.Value.Equals(il.TaxTotal[0].TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value))
                {
                    taxSubtotal.TaxableAmount.Value += il.TaxTotal[0].TaxSubtotal[0].TaxableAmount.Value;
                    //taxSubtotal.TaxAmount.Value += il.TaxTotal[0].TaxSubtotal[0].TaxAmount.Value;
                    taxSubtotal.TaxAmount.Value = Decimal.Round(taxSubtotal.TaxableAmount.Value * taxSubtotal.TaxCategory.Percent.Value / 100, invoiceTotalsRoundingDecimalPlaces);
                    return;
                }
            }
            taxSubtotalList.Add(createTaxSubtotal(il.TaxTotal[0].TaxSubtotal[0].TaxableAmount.Value,il.TaxTotal[0].TaxSubtotal[0].TaxAmount.Value,
                il.TaxTotal[0].TaxSubtotal[0].TaxCategory.ID.Value, il.TaxTotal[0].TaxSubtotal[0].TaxCategory.Percent.Value, il.TaxTotal[0].TaxSubtotal[0].TaxCategory.TaxScheme.ID.Value));
            
        }

        private void addContact(PartyType party, String email, String moreInfo)
        {
            party.Contact = new ContactType();
            if (!String.IsNullOrEmpty(email))
            {
                party.Contact.ElectronicMail = new ElectronicMailType();
                party.Contact.ElectronicMail.Value = email;
            }
            if (!String.IsNullOrEmpty(moreInfo))
            {
                party.Contact.Note = new NoteType[1];
                party.Contact.Note[0] = new NoteType();
                party.Contact.Note[0].Value = moreInfo;
            }
        }

        private PartyLegalEntityType[] createPartyLegalEntity(String entityName, String entityAddress, String entityTown,
            String entityPostalCode, String entityRegion, String entityCountryCode)
        {
            PartyLegalEntityType[] ple = new PartyLegalEntityType[1];
            ple[0] = new PartyLegalEntityType();
            ple[0].RegistrationName = new RegistrationNameType();
            ple[0].RegistrationName.Value = entityName;
            ple[0].RegistrationAddress = new AddressType();
            createAddress(ple[0].RegistrationAddress, entityAddress, entityTown, entityPostalCode, entityRegion, entityCountryCode);
            return ple;
        }

        public void createAddress(AddressType address, String entityAddress, String entityTown,
            String entityPostalCode, String entityRegion, String entityCountryCode)
        {
            address.StreetName = new StreetNameType();
            address.StreetName.Value = entityAddress;
            address.CityName = new CityNameType();
            address.CityName.Value = entityTown;
            if (!String.IsNullOrEmpty(entityPostalCode))
            {
                address.PostalZone = new PostalZoneType();
                address.PostalZone.Value = entityPostalCode;
            }
            if (!String.IsNullOrEmpty(entityRegion))
            {
                address.CountrySubentity = new CountrySubentityType();
                address.CountrySubentity.Value = entityRegion;
            }
            address.Country = new CountryType();
            address.Country.IdentificationCode = new IdentificationCodeType();
            address.Country.IdentificationCode.listID = Const_LISTID_COUNTRY;
            address.Country.IdentificationCode.listAgencyID = Const_AGENCYID_UNECE;
            address.Country.IdentificationCode.Value = entityCountryCode;
        }

        private PartyTaxSchemeType[] createPartyTaxScheme(String VATiD)
        {
            PartyTaxSchemeType[] ptsArray = new PartyTaxSchemeType[1];
            PartyTaxSchemeType pts = new PartyTaxSchemeType();
            ptsArray[0] = pts;
            pts.CompanyID = new CompanyIDType();
            pts.CompanyID.schemeID = Const_SPANISH_SCHEMEID_VAT;
            pts.CompanyID.schemeAgencyID = Const_AGENCYID_AEAT;
            pts.CompanyID.Value = VATiD;
            pts.TaxScheme = new TaxSchemeType();
            pts.TaxScheme.ID = new IDType();
            pts.TaxScheme.ID.schemeID = Const_SCHEMEID_VAT;
            pts.TaxScheme.ID.schemeAgencyID = Const_AGENCYID_UNECE;
            pts.TaxScheme.ID.Value = "VAT";
            return ptsArray;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StreamReader getStreamReader()
        {
            
            XmlSerializer mySerializer = new XmlSerializer(typeof(InvoiceType));
            StringWriter stringWriter = new StringWriter();

            MemoryStream ms = new MemoryStream();

            mySerializer.Serialize(ms, inv);

            ms.Position = 0;
            return new StreamReader(ms);
        }

    }    
}
