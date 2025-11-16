import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { BadgeModule } from 'primeng/badge';
import { CardModule } from 'primeng/card';
import { ChartModule } from 'primeng/chart';
import { ChartPieItem } from '../../../../core/interfaces/chart-pie-item.interface';

@Component({
  selector: 'app-chart-pie',
  standalone: true,
  imports:  [CommonModule, ChartModule, CardModule, BadgeModule],
  templateUrl: './chart-pie.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChartPieComponent implements OnChanges  {
  @Input() title: string = 'Título';
  @Input() data: ChartPieItem[] = [];

  @Input() total: number | null = null;

  pieData: any;
  pieOptions: any;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data']) {
      this.setupChart();
    }
  }

  private setupChart() {
    this.pieData = {
      labels: this.data.map(d => d.label),
      datasets: [
        {
          data: this.data.map(d => d.value),
          backgroundColor: ['#42A5F5','#66BB6A','#FFA726','#AB47BC','#FF7043']
        }
      ]
    };

    this.pieOptions = {
      responsive: true,
      maintainAspectRatio: false,
    };
  }
}
