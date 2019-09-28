<template>
    <bwc-export-button 
    :file-name="fileName"
    :start-export="startExport"
    :html="html"
    @export-complete="exportComplete">
        <div slot="content">        
            <div style="font-size:13px;">
                <div>
                    <span class="label">INVOICE No: </span>  <b>{{values.InvoiceNo}}</b>
                </div>        
                <div>
                    <span class="label">INVOICE SALE: </span> <b>{{values.InvoiceSale}}</b>
                </div>
                <div>
                    <span class="label">DATE: </span> <b>{{values.InvoiceDate|date}}</b>
                </div>
                <div>
                    <span class="label">PAYMENT TERM: <b>Cash</b> </span>
                </div>
                <div>
                    <span class="label">ABN: </span> <b>{{values.ABN}}</b>
                </div> 
                <div>
                    <span class="label">REF: </span> <b>{{values.Ref}}</b>
                </div>        
            </div>
        </div>
        <div slot="footer">
            <div style="font-size:12px;">
                <p style="margin-left:450px;">
                    Subtotal:       {{values.Subtotal|currency}}
                </p>
                <p style="margin-left:450px;">
                    GST:            {{values.GST|percent}}
                </p>
                <p style="margin-left:450px;">
                    TOTAL INC GST:  {{values.TotalIncGST|currency}}
                </p>
                <p style="margin-left:450px;">
                    AMOUNT APPLIED:  {{values.AmountApplied|currency}}
                </p>
                <p style="margin-left:485px;">
                    <b class="label">BALANCE DUE: {{values.BalanceDue|currency}} </b>
                </p>
            </div>
            <p>PAYMENT TO BE MAKE BY</p>
            <p style="font-size:12px;">
                <span>CHEQUE OR BANK TRANSFER TO</span> 
                <br>
            <span>B WINDOW COVERS</span>  
                <br>
                <span>COMMONWEALTH BANK</span> 
                <br>
                <span>BSB: 063 - 171</span> 
                <br>
                <span>ACCOUNT NUMBER: 1095 3748</span>
            </p>
        </div>
    </bwc-export-button>
</template>

<script>
import BwcExportButton from "@/components/controls/Export.vue"
import formater from "@/plugin/formater"
import exporter from "@/plugin/exporter" 

export default {
    name:'OrderInvoiceExport',
    components:{
        BwcExportButton
    },
    props:{
        isExport:{
            type:Boolean,
            default:false
        },
        products:{
            type:Array
        },
        components:{
            type:Array
        },
        services:{
            type:Array
        },
        fileName:{
            type:String
        },
        values:{
            type:Object
        }
    },
    data(){
        return({
            startExport:false,
            html:null,
        })
    },
    watch:{
        isExport(val){
            if(val){
                this.generateHtml().then(()=>{                        
                    setTimeout(()=>{
                        this.startExport = true
                    },200)  
                })   
            }
        }
    },
    methods:{
        exportComplete(){
            this.startExport=false
            this.$emit('export-complete') //raise event when export complete
        },
        async generateHtml(){
            let div =  exporter.element.div.cloneNode(true);
            let ehtml = exporter.element.html.cloneNode(true);
            let body = exporter.element.body.cloneNode(true);
            let header = exporter.element.header.cloneNode(true);

            //create container
            let divContainer = div.cloneNode(true)

            let styles = {
                fontSize: "9px",
                width: "100%"
            }

            // render products
            if(this.products.length > 0){
                let productTitle = div.cloneNode(true) 
                productTitle.style.cssText = "margin-top:30px"
                productTitle.innerHTML = 'Products'         
                divContainer.appendChild(productTitle)

                var porductHeader = [
                    ['Name','ProductName'],
                    ['Color','ColorName'],
                    ['Quantity','Quantity',formater.number],
                    ['Price','UnitPrice',formater.currency]
                ]
                let productHtml = await exporter.readData(this.products,porductHeader,styles)
                divContainer.appendChild(productHtml)
            } 
        
            // render components 
            if(this.components.length > 0){
                let componentTitle = div.cloneNode(true)                
                componentTitle.innerHTML = 'Other Items'        
                divContainer.appendChild(componentTitle)

                var componentHeaders = [
                    ['Name','ComponentCode'],
                    ['Color','ColorName'],
                    ['Quantity','Quantity',formater.number],
                    ['Price','Price',formater.currency]
                ]
                let componentTable = await exporter.readData(this.components,componentHeaders,styles)
                divContainer.appendChild(componentTable)
            }

            // render services
            if(this.services.length > 0){
                let serviceTitle = div.cloneNode(true)                
                serviceTitle.innerHTML = 'Services'        
                divContainer.appendChild(serviceTitle)

                var serviceHeaders = [
                    ['Task','Task'],
                    ['Quantity','Quantity',formater.number],
                    ['Charge','Charge',formater.currency],
                    ['Service Date','ServiceDate',formater.date],
                    ['Service Time','ServiceTime',formater.time]
                ]
                let serviceTable = await exporter.readData(this.services,serviceHeaders,styles)
                divContainer.appendChild(serviceTable)
            }

            // append conent
            body.appendChild(divContainer)
            //append header
            ehtml.appendChild(header);
            //append body
            ehtml.appendChild(body)
            //set container to object html
            this.html = ehtml
        }
    }
}
</script>