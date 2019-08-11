<template>
    <el-row>
        <el-form :model="form" ref="form" status-icon>
            <el-row >
                <el-col :span="type == 'edit'?24:20">                        
                    <bwc-loading :loading="loading">
                        <el-row :gutter="20">
                            <el-col :span="12">
                                <el-form-item label="Product code"  prop="ProductCode" 
                                :rules="rules.Required">
                                    <el-input v-model="form.ProductCode" 
                                    auto-complete="off" ></el-input>
                                </el-form-item>
                                <el-form-item label="Category" prop="CategoryId" 
                                :rules="rules.Required">
                                    <el-select v-model="form.CategoryId" 
                                    filterable
                                    placeholder="Please select a category"
                                    class="textbox-fs"
                                    @change="handleCategoryChanged(form.CategoryId)">
                                        <el-option v-for="cat in categories" 
                                        :label="cat.CategoryName" 
                                        :value="cat.Id"
                                        :key="cat.Id"></el-option>
                                    </el-select>
                                </el-form-item>
                                <el-form-item label="Status" prop="ActiveStatus" 
                                :rules="rules.Required">
                                    <el-select v-model="form.ActiveStatus" 
                                    filterable
                                    placeholder="Please select a status"
                                    class="textbox-fs">
                                        <el-option v-for="sta in status" 
                                        :label="sta.label" 
                                        :value="sta.value"
                                        :key="sta.value"></el-option>
                                    </el-select>
                                </el-form-item>                                
                            </el-col>
                            <el-col :span="12">
                                <el-form-item label="Product name" prop="ProductName" 
                                :rules="rules.Required">
                                    <el-input v-model="form.ProductName" auto-complete="off" ></el-input>
                                </el-form-item>
                                <el-form-item label="Notes" >
                                    <el-input
                                        type="textarea"
                                        :rows="5"
                                        placeholder="Please input"
                                        v-model="form.Notes">
                                    </el-input>
                                </el-form-item>
                            </el-col>                            
                        </el-row>
                        <el-row v-if="categoryDetail.CategoryCode != null">
                            <el-col :span="24">
                                <bwc-roller-blind-form-create 
                                :id="form.CategoryId" 
                                :data="categoryDetail" 
                                @doSubmit="saveData"
                                @doCancel="handleCancel"
                                v-if="categoryDetail.CategoryCode.indexOf('ROLLERBLIND') !== -1">
                                </bwc-roller-blind-form-create>

                                <bwc-zip-screen-form-create 
                                :id="form.CategoryId" 
                                :data="categoryDetail" 
                                @doSubmit="saveData"
                                @doCancel="handleCancel"
                                v-else-if="categoryDetail.CategoryCode.indexOf('ZIPSCREEN') !== -1">
                                </bwc-zip-screen-form-create>

                                <bwc-fly-screen-form-create 
                                :id="form.CategoryId" 
                                :data="categoryDetail"
                                @doSubmit="saveData" 
                                @doCancel="handleCancel"
                                v-else-if="categoryDetail.CategoryCode.indexOf('FLYSCREEN') !== -1">
                                </bwc-fly-screen-form-create>

                                <bwc-security-fly-door-form-create 
                                :id="form.CategoryId" 
                                :data="categoryDetail" 
                                @doSubmit="saveData"
                                @doCancel="handleCancel"
                                v-else-if="categoryDetail.CategoryCode.indexOf('SECURITY') !== -1 
                                || categoryDetail.CategoryCode.indexOf('FLYDOOR') !== -1">
                                </bwc-security-fly-door-form-create>

                                <bwc-rs-form-create 
                                :id="form.CategoryId" 
                                :data="categoryDetail" 
                                @doSubmit="saveData"
                                @doCancel="handleCancel"
                                v-else-if="categoryDetail.CategoryCode.indexOf('RS') !== -1">
                                </bwc-rs-form-create>
                                
                                <bwc-category-form-create 
                                :id="form.CategoryId" 
                                :data="categoryDetail"
                                @doSubmit="saveData" 
                                @doCancel="handleCancel"
                                v-else></bwc-category-form-create>
                            </el-col>
                        </el-row>
                        <!-- <el-row>
                            <el-col :span="24">
                                <el-row class="text-right" v-if="type== 'create'">
                                    <el-button type="primary" :disabled="processing" @click="saveData">
                                        Continue
                                        <i v-if="processing" class="el-icon-loading"></i> 
                                    </el-button> 
                                </el-row>
                                <bwc-modal-footer v-else>
                                    <el-button @click="handleCancel">Cancel</el-button>
                                    <el-button type="primary" @click="saveData">
                                        Save
                                        <i v-if="processing" class="el-icon-loading"></i>
                                    </el-button>
                                </bwc-modal-footer>
                            </el-col>
                        </el-row> -->
                    </bwc-loading>
                </el-col>
            </el-row>            
        </el-form>
    </el-row>
</template>

<script>
import ValidattionRules from '@/plugin/rule'
import BwcCategoryFormCreate from "@/components/category/CategoryFormCreate.vue";
import BwcRollerBlindFormCreate from "@/components/category/CategoryFormCreate.RollerBlind.vue";
import BwcZipScreenFormCreate from "@/components/category/CategoryFormCreate.ZipScreen.vue";
import BwcFlyScreenFormCreate from "@/components/category/CategoryFormCreate.FlyScreen.vue";
import BwcSecurityFlyDoorFormCreate from "@/components/category/CategoryFormCreate.Security.FlyDoor.vue";
import BwcRsFormCreate from "@/components/category/CategoryFormCreate.RS.vue";

export default {
    name:"ProductFormCreate",
    props:['type','data','id'],
    components:{
        BwcCategoryFormCreate,
        BwcRollerBlindFormCreate,
        BwcZipScreenFormCreate,
        BwcFlyScreenFormCreate,
        BwcSecurityFlyDoorFormCreate,
        BwcRsFormCreate
    },
    data(){
        return({
            loading:false,
            processing:false, 
            rules:ValidattionRules      
        })
    },
    computed:{
        form(){
            return this.$store.getters['product/info']
        },
        status(){
            return this.$store.getters.status
        },
        categories(){
            return this.$store.getters['category/all']
        },
        categoryDetail(){
            return this.$store.getters['category/info']
        }
    },
    created(){
        if(this.type == 'create')
        {
            this.$store.dispatch('product/clearInfo')
            this.$store.dispatch('category/clearInfo')
        }else{
            this.$store.dispatch('category/pullInfo',this.form.CategoryId)
        }

        this.$store.dispatch('supplier/pullAll')  
        this.$store.dispatch('category/pullAll')  
    },
    methods:{
        saveData(e){
            let self = this
            self.processing=true
            self.isDisabled=true
            //validate data
            
            self.validateForm('form')
            .then(_=>{
                if(_.valid){
                    //save data
                    if(self.id > 0){
                        self.$store.dispatch('product/update',{id:self.id,data:self.form})
                        .then(_=>{
                            this.$store.dispatch('product/push',this.form)  
                            this.handleCancel();
                            self.processing=false
                            self.isDisabled=false
                        })  
                    }else{
                        self.$store.dispatch('product/insert',self.form)
                        .then(_=>{
                            window.location.href = window.bwc.rootUrl + "/product/list";
                        })
                    }
                }else{
                    self.processing=false
                    self.isDisabled=false
                    return false
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
        handleCancel(){
            if(this.type == 'create')
            {
                window.location.href = window.bwc.rootUrl + "/product/list";
            }else{
                this.$emit('close-modal')
            }
        },
        handleCategoryChanged(id){
            this.$store.dispatch('category/pullInfo',id)
        }
    }
}
</script>

