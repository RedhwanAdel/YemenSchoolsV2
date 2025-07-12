import { Directive, HostBinding } from '@angular/core';

@Directive({
  selector: '[appMainBtn]',
  standalone: true
})
export class MainBtnDirective {

  @HostBinding('class')
  elementClass = 'app-main-btn';

  @HostBinding('style.transition') transition = '0.3s';
}
