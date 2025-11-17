import { Injectable } from '@angular/core';
import { MessageService, ToastMessageOptions } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class ToastService {

  constructor(private messageService: MessageService) {}

  showError(summary: string, detail?: string) {
    this.messageService.add({ severity: 'error', summary, detail, life: 5000 });
  }

  showSuccess(summary: string, detail?: string) {
    this.messageService.add({ severity: 'success', summary, detail, life: 3000 });
  }

  showInfo(summary: string, detail?: string) {
    this.messageService.add({ severity: 'info', summary, detail, life: 3000 });
  }

    showWarn(summaryOrArray: string | string[], detail?: string) {
    if (Array.isArray(summaryOrArray)) {
      const msgs: ToastMessageOptions[] = summaryOrArray.map(msgText => ({
        severity: 'warn',
        summary: msgText,
        detail: detail,
        life: 5000
      }));
      this.messageService.addAll(msgs);
    } else {
      const msg: ToastMessageOptions = { severity: 'warn', summary: summaryOrArray, detail, life: 5000 };
      this.messageService.add(msg);
    }
  }
}
