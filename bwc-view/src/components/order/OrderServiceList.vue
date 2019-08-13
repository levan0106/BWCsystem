<template>
    <el-row>
        <el-button-group>            
            <el-button 
            icon="el-icon-plus" 
            type="primary" 
            @click="openModal"
            :disabled="order.Step >= 3"> Service</el-button>

            <!-- <el-button 
            icon="el-icon-plus" 
            type="success">  
                <router-link :to="{name:'serviceCreate'}">							
                    Create New Service
                </router-link>	 
            </el-button>  -->
        </el-button-group>

        <bwc-order-add-service-modal 
        :orderId="orderId"
        :serviceId="serviceId"
        :open="isOpen"
        @close-modal="isOpen=false"
        @save-data="reloadService" />

        <bwc-grid-data
        :data="data"
        :loading="loading"
        no-paging
        :default-sort = "{prop: 'Id', order: 'descending'}"
        @selection-change="handleSelectionChange">
            <el-table-column
            type="selection"
            width="35"
            v-if="order.Step == 2">
            </el-table-column>

            <el-table-column
            prop="Id"
            width="40"
            type="index">
            </el-table-column>

            <!-- <el-table-column
            prop="ServiceId"
            label="Service ID">
            </el-table-column> -->
            
            <el-table-column
            prop="Task"
            label="Task">
                <template slot-scope="scope">
                    {{scope.row.Task|nullValue}}
                </template>
            </el-table-column>

            <el-table-column
            prop="Quantity"
            label="Quantity">					  
            </el-table-column>
            
            <el-table-column
            prop="Charge"
            label="Charge">		
                <template slot-scope="scope">
                    {{scope.row.Charge|currency}}
                </template>			  
            </el-table-column>
            
            <el-table-column
            prop="ServiceDate"
            label="Service Date">
                <template slot-scope="scope">
                    {{scope.row.ServiceDate|date}}
                </template>
            </el-table-column>

            <el-table-column
            prop="ServiceTime"
            label="Service Time">
                <template slot-scope="scope">
                    {{scope.row.ServiceTime|time}}
                </template>
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
import BwcOrderAddServiceModal from '@/components/order/OrderAddServiceModal.vue'
import BwcInputAction from '@/components/controls/InputAction.vue'

export default {
    name:"OrderServiceList",
    components:{
        BwcOrderAddServiceModal,
        BwcGridData,
        BwcInputAction
    },
    props:['orderId','order','from'],
    data(){
        return({
            isOpen:false,
            loading:false,
            serviceId:0
        })
    },
    computed:{
        data(){
            return this.$store.getters['order/allServices']
        },
    },
    methods:{
        handleSelectionChange(val){
            this.$emit('selection-change',val)
        },
        openModal(){
            //force reload page
            this.$store.dispatch('forceReloadPage')

            this.isOpen=true
            this.serviceId=0
        },
        reloadService(){
            this.isOpen=false;
            this.$store.dispatch('order/pullAllServices',this.orderId)
            this.$emit('save-data');
        },
        editItem(id){
            //force reload page
            this.$store.dispatch('forceReloadPage')

            this.isOpen=true
            this.serviceId=id
        },
        doDelete:function(id){
            let self = this
            this.$store.dispatch('order/deleteService',id)
            .then(()=>{
                this.$emit('save-data')
                this.$store.dispatch('order/pullAllServices',self.orderId)
            })
        },
    }
}
</script>

<style>

</style>
