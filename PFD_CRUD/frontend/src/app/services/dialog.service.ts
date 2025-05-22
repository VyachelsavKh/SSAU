import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DialogService {
  confirm(message: string): Promise<boolean> {
    return new Promise((resolve) => {
      const confirmed = window.confirm(message);
      resolve(confirmed);
    });
  }

  alert(message: string): Promise<void> {
    return new Promise((resolve) => {
      window.alert(message);
      resolve();
    });
  }
}
