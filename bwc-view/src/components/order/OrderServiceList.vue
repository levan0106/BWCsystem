<template>
    <el-row>
        <el-button-group>            
            <el-button 
            icon="el-icon-plus" 
            type="primary" 
            @click="openModal"
            :disabled="order.Step >= 3"> Service</el-button>

            <el-button 
            icon="el-icon-plus" 
            type="success">  
                <router-link :to="{name:'serviceCreate'}">							
                    Create New Service
                </router-link>	 
            </el-button> 
        </el-button-group>

        <bwc-order-add-component-modal 
        :data="data"  
        :component-list="componentList"
        :order-id="orderId"
        :order="order"
        :component-id="componentId"
        :open="isOpen"
        @close-modal="isOpen=false"
        @save-data="reloadComponent"/>

        <bwc-grid-data
        :data="data"
        :loading="loading"
        no-paging
        :default-sort = "{prop: 'Id', order: 'descending'}"
        @selection-change="handleSelectionChange">
            <el-table-column
            type="selection"
            width="35"
            v-if="from == 'order' && order.Step == 2">
            </el-table-column>

            <el-table-column
            prop="Id"
            width="40"
            type="index"
            >
            </el-table-column>

            <el-table-column
                prop="ServiceId"
                label="Service ID">
            </el-table-column>
            
            <el-table-column
                prop="Task"
                label="Task">
                <template slot-scope="scope">
                    {{scope.row.Description|nullValue}}
                </template>
            </el-table-column>

            <el-table-column
                prop="Quantity"
                label="Quantity">					  
            </el-table-column>
            
            <el-table-column
                prop="Charge"
                label="Charge">					  
            </el-table-column>
            
            <el-table-column
                prop="ServiceDate"
                label="Service Date">
            </el-table-column>

            <el-table-column
                prop="ServiceTime"
                label="Service Time">
            </el-table-column>

            <el-table-column
                fixed="right"
                label="Operations"
                width="100"
                class-name="no-print">
                <template slot-scope="scope">
                    <el-button type="text" class="el-icon-edit-outline" 
                    @click="editItem(scope.row.Id)" ></el-button> | 
                    <bwc-delete-item :delete-id="scope.row.Id" icon="el-icon-delete"
                        @do-delete="doDelete(scope.row.Id)"
                        :disabled="order.Step >= 3"
                    ></bwc-delete-item>
                </template>
            </el-table-column>
        </bwc-grid-data>  
    </el-row>
</template>

<script>
import BwcGridData from '@/components/common/GridData.vue'
import BwcOrderAddComponentModal from '@/components/order/OrderAddComponentModal.vue'
import BwcInputAction from '@/components/controls/InputAction.vue'
import functions from '@/plugin/function'

export default {
    name:"OrderServiceList",
    components:{
        BwcOrderAddComponentModal,
        BwcGridData,
        BwcInputAction
    },
    props:['orderId','supplierId','order','from'],
    data(){
        return({
            isOpen:false,
            loading:false,
            componentId:0
        })
    },
    computed:{
        data(){
            return this.$store.getters['order/allComponent']
        },
        componentList(){
            return this.$store.getters['component/all']
        },
    },
    watch:{
        supplierId(val){
            this.getComponentList(val)
        },
    },
    mounted(){   
        // //get all components by order      
        // this.$store.dispatch('order/pullAllComponent',this.orderId)
        // .then(_=>{
        //     this.loading=false
        // })
    },
    methods:{
        handleSelectionChange(val){
            this.$emit('selection-change',val)
        },
        getComponentList(){            
            //get component list by supplier
            this.$store.dispatch('component/pullAllBySupplier',0)
            .then(()=>{
                this.loading=false
            })
        },
        openModal(){
            //force reload page
            this.$store.dispatch('forceReloadPage')

            this.isOpen=true
            this.componentId=0
             this.getComponentList(this.supplierId)
        },
        reloadComponent(){
            this.isOpen=false;
            this.$store.dispatch('order/pullAllComponent',this.orderId)
            //this.$emit('save-data');
        },
        editItem(id){
            //force reload page
            this.$store.dispatch('forceReloadPage')

            this.isOpen=true
            this.componentId=id
        },
        doDelete:function(id){
            let self = this
            this.$store.dispatch('order/deleteComponent',id)
            .then(()=>{
                this.$emit('save-data')
                this.$store.dispatch('order/pullAllComponent',self.orderId)
            })
        },
        // updateQuantityReceived(id,number){
        //     let data = {Id:id,Received:number}
        //     this.$store.dispatch('order/updateComponent',{id:id,data:data})
        //     .then(()=>{
        //         this.$emit('save-data');
               
        //         //show message
        //         functions.$this = this
        //         functions.message.success()
        //     })
        // }
    }
}
</script>

<style>

</style>
