import { Component, Inject } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
declare var $:any;

@Component({
  selector: 'app-product-data',
  templateUrl: './product.component.html'
})
export class ProductComponent {
  public productView: ProductView;
  public categories: Category[];
  public formAvailable: boolean;

  public productName: string;
  public categoryType: string;

  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.formAvailable = false;
    this.getProducts(null, 1);
    this.getCategory();
  }

  public getProducts(type: string, page: number){
    let params = new HttpParams().set('productPage', page.toString());

    if (type && type == this.productView.currentType){
      type = null
    }

    if (type){
      params = params.set("category", type);
    }
    this.http.get<ProductView>(this.baseUrl + 'product', {
      params: params
    }).subscribe(result => {
      this.productView = result;
    }, error => console.error(error));
  }

  public getCategory(){
    this.http.get<Category[]>(this.baseUrl + 'category').subscribe(result => {
      this.categories = result;
    }, error => console.error(error));
  }

  public  showForm(){
    this.formAvailable = true;
  }

  public addProduct(){

    let params = new HttpParams().set('name', this.productName)
      .set('category', this.categoryType)

    this.http.get<ProductView>(this.baseUrl + 'product/SaveProduct', {
      params: params
    }).subscribe(result =>{
      this.productView = result;
      this.formAvailable = false;
    }, error => console.error(error));
  }


  public pageRange(){
    let pages = Math.ceil(this.productView.pagingInfo.totalItems / this.productView.pagingInfo.itemsPerPage);
    let range = [];

    for (let i = 1; i <= pages; i++){
      range.push(i);
    }

    return range;
  }
}

interface ProductView{
  products: Product[];
  pagingInfo: PagingInfo;
  currentType: string;
}

interface Product {
  id: number;
  name: string;
  category: Category;
}

interface Category {
  id: number;
  type: string;
}

interface PagingInfo {
  currentPage: number;
  totalItems: number;
  itemsPerPage: number;
  totalPages: number;
}
