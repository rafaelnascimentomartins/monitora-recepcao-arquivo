import { Component, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule, ToastModule],
  template: `<p-toast></p-toast>`,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ToastComponent {
  constructor(public messageService: MessageService) {}
}
