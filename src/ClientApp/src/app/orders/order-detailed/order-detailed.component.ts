import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { OrdersService } from '../orders.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { OrderToReturn } from 'src/app/shared/models/order-to-return';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
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
    private bcService: BreadcrumbService,
    private cdr: ChangeDetectorRef
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
          this.cdr.markForCheck();
        }
      });
    }
  }

  saveAsPDF(): void {
    const data = document.getElementById('order-details');
    const button = document.querySelector('.search-button') as HTMLElement;
    if (data && button) {
      button.style.display = 'none';
      this.cdr.detectChanges();
      html2canvas(data, { useCORS: true, scale: 2 }).then(canvas => {
        const imgWidth = 208;
        const imgHeight = canvas.height * imgWidth / canvas.width;
        const contentDataURL = canvas.toDataURL('image/png');
        const pdf = new jsPDF('p', 'mm', 'a4');
        pdf.addImage(contentDataURL, 'PNG', 0, 0, imgWidth, imgHeight);
        pdf.save('order-details.pdf');
        button.style.display = 'block';
        this.cdr.markForCheck();
      }).catch(error => {
        console.error('Error creating canvas:', error);
        button.style.display = 'block';
        this.cdr.markForCheck();
      });
    } else {
      console.error('Element not found');
    }
  }
}