Samples for eFacturesCat API for .Net
=====================================

- SamplesCommon - Project with common methods for samples
- Facturae32Sign - Signs all xml (facturae 32 unsigned) files located in "inbox" folder and save signed files in "signed" and move unsigned to "processed" (if no error) or to "not_processed" (if error).
- PimefacturaDeliver - Deliver to "pimefactura" all signed xml (facturae 32) files located in "signed" folder and move files to to "processed" (if no error) or to "not_processed" (if error).
- EmailDeliver - Deliver via SMTP Server all signed xml (facturae 32) files located in "signed" folder and move files to to "processed" (if no error) or to "not_processed" (if error).
- PimefacturaSignAndDeliver - Signs all xml (facturae 32 unsigned) files located in "inbox" folder and deliver to "pimefactura", saving signed files in "signed" and move unsigned to "processed" (if no error) or to "not_processed" (if error).
- GenerateTransformSignAndDeliverPimefactura - Generate one "semantic" invoice and one "semantic" CreditNote. Transforms both to facturae 3.2. Signs each and deliver to pimefactura.