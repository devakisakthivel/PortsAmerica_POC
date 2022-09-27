import java.awt.*;
import javax.imageio.ImageIO;
import javax.print.PrintService;
import javax.print.PrintServiceLookup;
import javax.print.attribute.HashPrintRequestAttributeSet;
import javax.print.attribute.standard.Destination;
import java.awt.print.*;
import java.io.File;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;


public class PrinterPOC implements Printable {
    static DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy_MM_dd_HH_mm_ss");
    static LocalDateTime now = LocalDateTime.now();
    static String currentTime = dtf.format(now);
    String imgPath;
    String pdfPath;



    @Override
    public int print(Graphics graphics, PageFormat pageFormat, int pageIndex) throws PrinterException {
    try{
        if (pageIndex > 0) {
            return NO_SUCH_PAGE;
        }

        Graphics2D g2d = (Graphics2D) graphics;
        Image img = null;
        g2d.setColor(Color.black);

        img = ImageIO.read(new File(imgPath));
        g2d.drawImage(img,19,0,null);

        g2d.setFont(new Font("Consolas", Font.BOLD, 12));
        g2d.drawString(spaces(10)+"Ports America", 4, 50);
        g2d.setFont(new Font("Consolas", Font.PLAIN, 10));
        g2d.drawString(spaces(6)+"Receipt No : xxxxxxx", 0, 70);
        g2d.drawString(spaces(6)+"Date : "+currentTime, 0, 80);
        g2d.drawString(spaces(6)+"Trucker Licence : xxxxxx", 0, 90);
        g2d.drawString(spaces(6)+"Truck Details : xxxxxx", 0, 100);
        g2d.drawString(spaces(10)+"Thank you! Visit Again!", 0, 130);

        g2d.translate(pageFormat.getImageableX(), pageFormat.getImageableY());


    }catch(Exception e){
        System.out.println("Exception:"+e);
    }
    return PAGE_EXISTS;
    }

    public String spaces(int num) {

        String sp = "";
        for(int i = 0; i < num; i++)
        sp += " ";

        return sp;

    }

    private static PrintService findPrintService(String printerName) {
         PrintService[] printServices = PrintServiceLookup.lookupPrintServices(null, null);
         for (PrintService printService : printServices) {
                if (printService.getName().trim().equals(printerName)) {
                    return printService;
                }
         }
         return null;
    }

    public void ListPrintServices(){
        PrintService[] printServices = PrintServiceLookup.lookupPrintServices(null, null);
        System.out.println("Number of print services: " + printServices.length);
        int i =1;
        for (PrintService printer : printServices) {
            System.out.println("Printer"+i+": " + printer.getName());
            i++;
        }
    }

    public PageFormat getPageFormat(){
        PrinterJob printerJob = PrinterJob.getPrinterJob();
        Paper paper = new Paper();
        PageFormat pageFormat = printerJob.defaultPage();
        final int MARGIN = 1;
        int width = 300;
        int height = 1000;

        paper.setImageableArea(MARGIN, MARGIN, width, height);
        pageFormat.setPaper(paper);

        pageFormat.setOrientation(PageFormat.PORTRAIT);
        return pageFormat;
    }

    public void PrintToPDF(String PrinterName , PrinterPOC printerPOC , PageFormat pageFormat){
        PrinterJob printerJob = PrinterJob.getPrinterJob();

        try {
            PrintService printService = printerPOC.findPrintService(PrinterName);

            printerJob.setPrintService(printService);
            HashPrintRequestAttributeSet attributes = new HashPrintRequestAttributeSet();

            attributes.add(new Destination(new File(pdfPath+"PortsAmericaPrinterPOC_"+currentTime+".pdf").toURI()));
            printerJob.setPrintable(printerPOC, pageFormat);
            printerJob.print(attributes);
        }catch (PrinterException ex) {
            ex.printStackTrace();
        }
    }

    public void PrintingInPrinter(String printerName , PrinterPOC printerPOC , PageFormat pageFormat){
        try {
            PrintService printServiceHP = printerPOC.findPrintService(printerName);
            PrinterJob job = PrinterJob.getPrinterJob();

            job.setPrintService(printServiceHP);
            job.setPrintable(printerPOC, pageFormat);
            job.print();
        }catch (Exception e){
            e.printStackTrace();
        }
    }

    public static void main(String args[]) {

        PrinterPOC printerPOC = new PrinterPOC();
        printerPOC.imgPath = args[2];
        printerPOC.pdfPath=args[3];

    //    printerPOC.ListPrintServices();
        PageFormat pageFormat = printerPOC.getPageFormat();
        if(args.length > 0) {
            printerPOC.imgPath = args[2];
            printerPOC.pdfPath=args[3];            				printerPOC.PrintingInPrinter(args[0],printerPOC,pageFormat);
            printerPOC.PrintToPDF(args[1] , printerPOC,pageFormat);
        }else
                System.out.println("Please give printer name as input variable");
    }
}
