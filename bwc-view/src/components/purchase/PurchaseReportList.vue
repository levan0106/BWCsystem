<template>
<el-row>
	<el-row v-show="data.length > 0" style="background: rgb(245, 244, 244);padding: 10px 20px;">
		<el-col :span="4" :offset="6">
			<bwc-form-control 
			label="Invoice Amount">{{totalData['TotalReceived']}}
			</bwc-form-control >
		</el-col>
		<el-col :span="4">
			<bwc-form-control 
			label="Paid">{{totalData['TotalPaid']}}
			</bwc-form-control>
		</el-col>
		<el-col :span="4">
			<bwc-form-control 
			label="Balance">{{totalData['Balance']}}
			</bwc-form-control>
		</el-col>
	</el-row>
    <bwc-grid-data
	highlight-current-row
	:data="data"
	:loading="loading"
	:show-summary="false"
	:index-text-summary="5"
	:index-show-summary="[6,8,9]"
	:value-null-summary="''"
	:pageSize="50"
	:default-sort = "{prop: 'Id', order: 'descending'}">	
		<el-table-column
		type="index"
		width="50">
		</el-table-column>	

		<el-table-column
		prop="Id"
		label="Order Id"
		width="130">
			<template slot-scope="scope">
					<router-link 
					:to="{name:'purchaseDetail',params:{id:scope.row.Id}}">
						{{ scope.row.Id }}
					</router-link>	
			</template>
		</el-table-column>

		<el-table-column
		prop="SupplierName"
		width="140"
		label="Supplier">
			<template slot-scope="scope">
				{{ scope.row.SupplierName|nullValue }}
			</template>
		</el-table-column>
		
		<el-table-column
		prop="OrderDate"
		width="160"
		label="Order Date">
			<template slot-scope="scope">
				{{ scope.row.OrderDate|datetime|nullValue }}
			</template>
		</el-table-column>

		<el-table-column
		prop="OrderRefNo"
		width="130"
		label="Ref/Name">
			<template slot-scope="scope">
				{{ scope.row.OrderRefNo|nullValue }}
			</template>
		</el-table-column>

		<el-table-column
		prop="DeliveryDate"
		width="160"
		label="Invoice Date">
			<template slot-scope="scope">
				{{ scope.row.DeliveryDate|date|nullValue }}
			</template>
		</el-table-column>
		
		<el-table-column
		prop="TotalReceived"
		label="Invoice Amount"
		width="110">
			<template slot-scope="scope">
				{{ scope.row.TotalReceived|currency }}
			</template>
		</el-table-column>

		<el-table-column
		prop="DatePaid"
		width="120"
		label="Paid Date">
			<template slot-scope="scope">
				{{ scope.row.DatePaid|date|nullValue }}
			</template>
		</el-table-column>

		<el-table-column
		prop="TotalPaid"
		label="Paid"
		width="120"	>
			<template slot-scope="scope">
				{{ scope.row.TotalPaid|currency }}
			</template>
		</el-table-column>

		<el-table-column
		prop="Balance"
		label="Balance">
			<template slot-scope="scope">
				{{ scope.row.Balance|currency }}
			</template>
		</el-table-column>
	</bwc-grid-data>
	
</el-row>
</template>

<script>
import BwcGridData from '@/components/common/GridData.vue'
export default {
	name:"PurchaseReportList",	
    components:{
		BwcGridData,
	},
	props:['loading','data'],
	computed:{
		totalData(){
			const sums = [];
			const cols = ['TotalReceived','TotalPaid','Balance'];
			cols.forEach((col,index) => {
				const values = this.getValuesOfColumn(col);
				if (!values.every(value => isNaN(value) || value == '')) {
					sums[col] = '$'+ values.reduce((prev, curr) => {
						const value = Number(curr)
						if (!isNaN(value)) {
							return prev + curr;
						} else {
							return prev;
						}
					}, 0).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
				} else {
					sums[col] = '-';
				}
			});
			return sums;
        }
	},
	methods:{
		getValuesOfColumn(column){
			const values = this.data.map(item => Number(item[column]));
			return values;
		}
	}
}
</script>
