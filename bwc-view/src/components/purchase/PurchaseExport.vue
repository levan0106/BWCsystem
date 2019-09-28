<template>
    <bwc-export-button 
    :file-name="fileName"
    :start-export="startExport"
    :html="html"
    @export-complete="exportComplete">
        <div slot="content">
            <div>
                Supplier Name: <b>{{values.SupplierName}}</b>
            </div>
            <div>
                Estimate Amount: <b>{{values.Amount|currency}}</b>
            </div>
        </div>
    </bwc-export-button>
</template>

<script>
import BwcExportButton from "@/components/controls/Export.vue"
import formater from "@/plugin/formater"
import exporter from "@/plugin/exporter" 

export default {
    name:'PurchaseExport',
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
                    },2000)  
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
            let span =  exporter.element.span.cloneNode(true);
            //let table = exporter.element.table.cloneNode(true);
            let ehtml = exporter.element.html.cloneNode(true);
            let body = exporter.element.body.cloneNode(true);
            let header = exporter.element.header.cloneNode(true);

            //create container
            let divContainer = div.cloneNode(true)

            let styles = {
                fontSize: "7px",
                width: "100%"
            }

            let tableElement = document.createElement("TABLE");
            tableElement.style.fontSize = styles.fontSize
            tableElement.style.width = styles.width

            // Product 
            var columns = [
                ['ID','Id','static'],
                ['WIDTH','Width','static'],
                ['DROP','Drop','static'],
                ['QTY','Quantity','static','40px'],                
                ['MATERIAL','MaterialName','static'],
                ['COLOUR','ColorName','static']
            ]

            // let productTitle = div.cloneNode(true) 
            // productTitle.innerHTML = "Products" 
            // divContainer.appendChild(productTitle)

            // Render product
            let productGrid = this.createDataGrid(this.products,columns,tableElement)
            divContainer.appendChild(productGrid)
            //End Product

            // Component

            let productComponentTitle = div.cloneNode(true) 
                productComponentTitle.innerHTML = "Purchase Items" 
                divContainer.appendChild(productComponentTitle)

            var componentColumns = [
                ['ID','Id'],
                ['CODE','ComponentCode'],
                ['Description','Description'], 
                ['COLOUR','ColorName'],               
                ['QTY','Quantity'],
                ['Unit Price','Price'],
                ['Discount (%)','Discount'],
            ]

            let componentGrid = this.createDataGrid(this.components,componentColumns,tableElement)
            divContainer.appendChild(componentGrid)

            //End Component

            // append conent
            body.appendChild(divContainer)
            //append header
            ehtml.appendChild(header);
            //append body
            ehtml.appendChild(body)
            //set container to object html
            this.html = ehtml
        },
        createDataGrid(dataSource, columns, orginalTable){
            let table = orginalTable.cloneNode(true)

            // render content
            let rowHeader = table.insertRow();
            dataSource.forEach((row, index)=>{
                let rowContent = table.insertRow();

                columns.forEach((cell, i)=>{ // go to through all cells  
                    
                    // item index  
                    let value=i  

                    if(cell[1] ==='Id'){
                        value = index + 1
                    }else{
                        for(var property in row){
                            if(cell[1] === property)
                            {
                                let func = cell[3]
                                if(typeof func === "function"){
                                    value = func(row[property])
                                }else{                                
                                    value = row[property]
                                }

                                break;
                            }
                        }      
                    }
                    
                    if(value != 0 || cell[2] === 'static'){
                        // First row then add header
                        if(index == 0){
                            let cellHeader = rowHeader.insertCell(); 
                            cellHeader.innerHTML = cell[0] 
                            if(cell[3] != undefined){
                                cellHeader.style.width = cell[3]
                            }
                        }

                        let cellContent = rowContent.insertCell();
                        cellContent.innerHTML = value                           
                        if(cell[3] != undefined){
                            cellContent.style.width = cell[3]
                        }
                    }
                })                
            })

            return table
        }
    }
}
</script>

