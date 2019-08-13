<template>
<div id="productcomponentmodal"> 
    <el-dialog title="Add service" :visible.sync="open" 
    class="modal modal-lg">        
        <el-form :model="form" ref="form" status-icon > 
            <bwc-loading :loading="loading">
                <el-row :gutter="20">
                    <el-col :span="12">    
                        <el-form-item label="Task"
                        prop="Task"
                        :rules="rules.Required">
                            <el-input v-model="form.Task">
                            </el-input>
                        </el-form-item> 

                        <el-form-item label="Quantity">
                            <el-input-number 
                            v-model="form.Quantity" 
                            auto-complete="off"
                            controls-position="right" class="input-number-fs"
                            :min=0>
                            </el-input-number>
                        </el-form-item>  

                        <el-form-item label="Charge">
                            <bwc-input-currency 
                            v-model="form.Charge" 
                            class="input-number-fs"
                            controls-position="right"
                            ></bwc-input-currency>
                        </el-form-item>  

                    </el-col>
                    <el-col :span="12">
                        <el-form-item label="Service Date">
                            <el-date-picker
                            v-model="form.ServiceDate"
                            type="date"
                            format="dd/MM/yyyy"
                            value-format="yyyy-MM-dd"
                            placeholder="Pick a day">
                            </el-date-picker>
                        </el-form-item>    

                        <el-form-item label="Service Time">
                            <el-time-picker
                            v-model="form.ServiceTime"
                            format="HH:mm"
                            value-format="HH:mm"
                            :picker-options="{
                                selectableRange:'07:00:00 - 20:30:00'
                            }"
                            placeholder="Select time">
                            </el-time-picker>
                        </el-form-item>
                        <!-- <el-form-item label="Service Time">
                            <el-time-select
                            v-model="ServiceTime"    
                            format="HH:mm"
                            value-format="HH:mm"
                            :picker-options="{
                                start: '08:30',
                                step: '00:15',
                                end: '18:30'
                            }"            
                            placeholder="Start time">
                            </el-time-select>
                        </el-form-item> -->
                    </el-col>
                </el-row>
            </bwc-loading> 
        </el-form>
        <bwc-modal-footer>
            <el-button @click="closeModal">Cancel</el-button>                                 
            <el-button v-if="serviceId > 0" type="primary" @click="saveData('update')">
                Save
                <i v-if="processing" class="el-icon-loading"></i>
            </el-button>
            <el-button v-else type="primary" @click="saveData('add')">
                Add
                <i v-if="processing" class="el-icon-loading"></i>
            </el-button>
        </bwc-modal-footer>
    </el-dialog>
</div>
</template>

<script>
import ValidattionRules from '@/plugin/rule'
import formater from "@/plugin/formater"

export default {
    name:"OrderAddServiceModal",
    props:['open','orderId','serviceId'],
    data(){
        return({
            loading:false,
            processing:false,
            formLabelWidth:'150px',
            form:{
                OrderId:this.orderId,
            },
            rules:ValidattionRules
        })
    }, 
    computed:{
        forceReloadPage(){
            return this.$store.getters.forceReloadPage
        },
        ServiceTime: {
            get: function() {
                return formater.time(this.form.ServiceTime)
            },
            set: function(modifiedValue) {                
                this.form.ServiceTime = modifiedValue
            }
        },
    },
    watch:{
        forceReloadPage(val){
            this.loadData()
        }
    },
    created(){
        this.loadData()
    },
    
    methods:{
        
        closeModal(){
            this.$emit('close-modal')
            
            // //force reload page
            // this.$store.dispatch('forceReloadPage')
        },
        loadData(){
            this.loading=true
            if(this.serviceId > 0){
                this.$store.dispatch('order/pullService',this.serviceId)
                .then(_=>{
                    this.loading=false
                    this.form = this.$store.getters['order/service']
                })
            }else{
                this.loading=false
                this.form={
                    OrderId:this.orderId,
                    Quantity:1,
                }
            }
        },
        saveData(actionType){
            let self = this
            this.processing = true

            let url = 'order/updateService'
            let param = {
                id:self.serviceId,
                data:self.form
            }
            if(actionType == 'add'){
                url = 'order/addService'
                param = self.form
            }

            self.validateForm('form')
            .then(_=>{
               if(_.valid){
                    self.processing=true
                    self.$store.dispatch(url,param)
                    .then(resolve=>{
                        this.$emit('save-data');
                        self.processing=false
                        // //force reload page
                        // this.$store.dispatch('forceReloadPage')
                    })
                    .catch(error=>{
                        self.processing=false
                    })
                }else{
                    self.processing=false
                }
            })
        },
        validateForm(formName) {
            return new Promise((resolve,reject)=>{
                let isValid=false
                this.$refs[formName].validate((valid) => {
                    isValid = valid
                });
                resolve({valid:isValid})
            })
        },
    }
}
</script>
