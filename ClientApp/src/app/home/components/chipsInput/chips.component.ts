import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {Component, ElementRef, Input, Output, ViewChild} from '@angular/core';
import {FormControl} from '@angular/forms';
import {MatAutocompleteSelectedEvent} from '@angular/material/autocomplete';
import {MatChipInputEvent} from '@angular/material/chips';
import {Observable} from 'rxjs';

@Component({
  selector: 'chips-input',
  templateUrl: 'chips.component.html',
  styleUrls: ['chips.component.css'],
})
export class ChipsInputComponent {
  visible = true;
  selectable = true;
  removable = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  wordsCtrl = new FormControl();

  @Input() words: string[] = [];
  @ViewChild('wordInput', {static: true}) wordInput: ElementRef<HTMLInputElement>;

  constructor() {
  }

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    // Add our fruit
    if (value) {
      this.words.push(value);
    }

    this.wordsCtrl.setValue(null);
  }

  remove(fruit: string): void {
    const index = this.words.indexOf(fruit);

    if (index >= 0) {
      this.words.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.words.push(event.option.viewValue);
    this.wordInput.nativeElement.value = '';
    this.wordsCtrl.setValue(null);
  }
}