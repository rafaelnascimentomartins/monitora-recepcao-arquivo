import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-menu-card',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush, 
  imports: [CardModule, ButtonModule, RouterModule],
  template: `
    <p-card [header]="title">
      <i [class]="icon" style="font-size: 2rem;"></i>
      <p>{{ description }}</p>
      <button pButton label="Abrir" [routerLink]="route"></button>
    </p-card>
  `,
  styles: [`
    p-card {
      width: 200px;
      margin: 1rem;
      text-align: center;
    }
    i {
      display: block;
      margin-bottom: 1rem;
    }
  `]
})
export class MenuCardComponent {
   @Input() title!: string;
   @Input() description!: string;
   @Input() icon!: string;
   @Input() route!: string;
}