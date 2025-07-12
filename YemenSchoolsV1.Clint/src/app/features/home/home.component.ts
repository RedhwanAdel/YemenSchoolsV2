import { Component, inject, OnInit } from '@angular/core';
import { HeroSectionComponent } from "./hero-section/hero-section.component";
import { AboutSectionComponent } from "./about-section/about-section.component";
import { HowWeWorkSectionComponent } from "./how-we-work-section/how-we-work-section.component";
import { ServicesSectionComponent } from "./services-section/services-section.component";
import { ServicesAltSectionComponent } from "./services-alt-section/services-alt-section.component";
import { CallToActionSectionComponent } from "./call-to-action-section/call-to-action-section.component";
import { PricingSectionComponent } from "./pricing-section/pricing-section.component";
import { FaqSectionComponent } from "./faq-section/faq-section.component";
import { TestimonialsSectionComponent } from "./testimonials-section/testimonials-section.component";
import { ContactSectionComponent } from "./contact-section/contact-section.component";
import { SchoolItemComponent } from "../schools/school-item/school-item.component";
import { TitelSectionComponent } from "../../shared/components/titel-section/titel-section.component";
import { SchoolService } from '../../core/services/school.service';
import { Pagination } from '../../shared/models/Pagination';
import { SchoolListItem } from '../../shared/models/school/school';
import { SchoolParams } from '../../shared/models/school/schoolParams';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeroSectionComponent, AboutSectionComponent, HowWeWorkSectionComponent, ServicesSectionComponent, ServicesAltSectionComponent, CallToActionSectionComponent, PricingSectionComponent, FaqSectionComponent, TestimonialsSectionComponent, ContactSectionComponent, SchoolItemComponent, TitelSectionComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  private schoolService = inject(SchoolService)
  schools: SchoolListItem[] = [];
  schoolParams = new SchoolParams();
  ngOnInit(): void {
    this.getSchools()
  }
  getSchools() {
    this.schoolParams.pageSize = 9
    this.schoolService.getSchools(this.schoolParams).subscribe({
      next: res => this.schools = res.data
    })
  }
}
