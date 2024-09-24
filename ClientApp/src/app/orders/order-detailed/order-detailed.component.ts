import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/shared/models/order';
import { OrdersService } from '../orders.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { OrderToReturn } from 'src/app/shared/models/order-to-return';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.sass']
})
export class OrderDetailedComponent implements OnInit {
  order?: OrderToReturn;
  displayedColumns: string[] = ['product', 'price', 'quantity', 'total'];

  constructor(
    private ordersService: OrdersService,
    private route: ActivatedRoute,
    private bcService: BreadcrumbService
  ) {
    this.bcService.set('@OrderDetailed', ' ');
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.ordersService.getOrderDetailed(id).subscribe({
        next: order => {
          this.order = order;
          this.bcService.set('@OrderDetailed', `Order# ${order.id} - ${order.status}`);
        }
      });
    }
  }

  saveAsPDF(): void {
    const data = document.getElementById('order-details');
    const button = document.querySelector('.search-button') as HTMLElement;
    if (data && button) {
      button.style.display = 'none';
      html2canvas(data).then(canvas => {
        const imgWidth = 208;
        const imgHeight = canvas.height * imgWidth / canvas.width;
        const contentDataURL = canvas.toDataURL('image/png');
        let pdf = new jsPDF('p', 'mm', 'a4');
        const position = 0;
        pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight);
        pdf.save('order-details.pdf');
        button.style.display = 'block';
      }).catch(error => {
        console.error('Error creating canvas:', error);
        
        button.style.display = 'block';
      });
    } else {
      console.error('Element not found');
    }
  }
}
