import tkinter as tk
from tkinter import ttk
from tkinter import filedialog
from enum import Enum, auto
import math
import time
from typing import Tuple, Callable, Any, List
from CustomElements.Element import Element
from CustomElements.LabelEntry import LabelEntry

class PompSettings(Element):
    def __init__(self, root : tk.Misc, gpio_pins : List[str], sensor_pins : List[str], pin_changed_handler : Callable[[str, str, str, str], Any], start_name : str, read_sensors : Callable[[], Tuple[int, int, int]], control_pomp : Callable[[int], Any]):
        self.__root = root
        
        super().__init__(ttk.Frame(self.__root, relief="groove"))
        
        self.__container = Element(ttk.Frame(self._element))

        self.__gpio_pins = gpio_pins
        self.__sensor_pins = sensor_pins
        self.__pin_changed_handler = pin_changed_handler

        self.__pin_picker_height = 20
        self.__elements_height_margin = 8

        self.__top_margin = 8
        self.__bottom_margin = 8
        self.__left_margin = 8
        self.__right_margin = 8

        self.__name = start_name
        self.__read_sensors = read_sensors
        self.__control_pomp = control_pomp
        self.__control_flag = False

        self.__create_elements()

    def __control(self):
        if self.__control_flag:
            button_state, sensor1_state, sensor2_state = self.__read_sensors()

            self.__control_pomp(button_state)

            self.__sensor1_val.get_element().configure(text = str(sensor1_state))
            self.__sensor2_val.get_element().configure(text = str(sensor2_state))

            #self.__root.after(100, self.__control)

    def __create_elements(self):
        self.__enter_name = LabelEntry(self.__container.get_element(), "Name:", 20, self.__name, height=self.__pin_picker_height)

        self.__pomp_label = Element(ttk.Label(self.__container.get_element(), text="Pomp pin:", anchor=tk.W), height = self.__pin_picker_height)
        self.__pomp_pin = Element(ttk.Combobox(self.__container.get_element(), values=self.__gpio_pins, state="readonly"), height = self.__pin_picker_height)

        self.__pomp_pin.get_element().current(0)
        self.__pomp_pin.get_element().bind("<<ComboboxSelected>>", self.__pomp_pin_changed)

        self.__button_label = Element(ttk.Label(self.__container.get_element(), text="Button pin:", anchor=tk.W), height = self.__pin_picker_height)
        self.__button_pin = Element(ttk.Combobox(self.__container.get_element(), values=self.__gpio_pins, state="readonly"), height = self.__pin_picker_height)

        self.__button_pin.get_element().current(0)
        self.__button_pin.get_element().bind("<<ComboboxSelected>>", self.__button_pin_changed)

        self.__sensor1_label = Element(ttk.Label(self.__container.get_element(), text="Bottom sensor pin:", anchor=tk.W), height = self.__pin_picker_height)
        self.__sensor1_pin = Element(ttk.Combobox(self.__container.get_element(), values=self.__sensor_pins, state="readonly"), height = self.__pin_picker_height)

        self.__sensor1_pin.get_element().current(0)
        self.__sensor1_pin.get_element().bind("<<ComboboxSelected>>", self.__sensor1_pin_changed)

        self.__sensor2_label = Element(ttk.Label(self.__container.get_element(), text="Top sensor pin:", anchor=tk.W), height = self.__pin_picker_height)
        self.__sensor2_pin = Element(ttk.Combobox(self.__container.get_element(), values=self.__sensor_pins, state="readonly"), height = self.__pin_picker_height)

        self.__sensor2_pin.get_element().current(0)
        self.__sensor2_pin.get_element().bind("<<ComboboxSelected>>", self.__sensor2_pin_changed)

        self.__min = LabelEntry(self.__container.get_element(), "Bottom sensor min value:", height=self.__pin_picker_height, write_restrict=self.__numbers_restrict, entry_max_len=3)
        self.__max = LabelEntry(self.__container.get_element(), "Top sensor max value:", height=self.__pin_picker_height, write_restrict=self.__numbers_restrict, entry_max_len=3)

        self.__sensor1_val_label = Element(ttk.Label(self.__container.get_element(), text="Bottom sensor value:", anchor=tk.W), height = self.__pin_picker_height)
        self.__sensor1_val = Element(ttk.Label(self.__container.get_element(), text="123", anchor=tk.W), height = self.__pin_picker_height)
        
        self.__sensor2_val_label = Element(ttk.Label(self.__container.get_element(), text="Top sensor value:", anchor=tk.W), height = self.__pin_picker_height)
        self.__sensor2_val = Element(ttk.Label(self.__container.get_element(), text="231", anchor=tk.W), height = self.__pin_picker_height)

    @staticmethod
    def __numbers_restrict(inp_str : str) -> str:
        cur_str = "".join(filter(str.isdigit, inp_str))
        
        if cur_str == "":
            return ""
        
        val = int(cur_str)

        if (val > 255):
            return "255"
        
        return cur_str

    def set_coordinates(self, x: int = 0, y: int = 0, width: int = None, height: int = None) -> Tuple[int]:
        remain_width = width - self.__left_margin - self.__right_margin
        remain_height = height - self.__top_margin - self.__bottom_margin
        self.__container.set_coordinates(self.__left_margin, self.__top_margin, remain_width, remain_height)

        _, cur_y = self.__enter_name.set_coordinates(0, 0, width=remain_width)

        cur_y += self.__elements_height_margin
        cur_x, _ = self.__pomp_label.set_coordinates(0, cur_y)
        remain_width2 = remain_width - cur_x - 5
        _, cur_y = self.__pomp_pin.set_coordinates(cur_x + 5, cur_y, remain_width2)

        cur_y += self.__elements_height_margin
        cur_x, _ = self.__button_label.set_coordinates(0, cur_y)
        remain_width2 = remain_width - cur_x - 5
        _, cur_y = self.__button_pin.set_coordinates(cur_x + 5, cur_y, remain_width2)

        cur_y += self.__elements_height_margin
        cur_x, _ = self.__sensor1_label.set_coordinates(0, cur_y)
        remain_width2 = remain_width - cur_x - 5
        _, cur_y = self.__sensor1_pin.set_coordinates(cur_x + 5, cur_y, remain_width2)

        cur_y += self.__elements_height_margin
        cur_x, _ = self.__sensor2_label.set_coordinates(0, cur_y)
        remain_width2 = remain_width - cur_x - 5
        _, cur_y = self.__sensor2_pin.set_coordinates(cur_x + 5, cur_y, remain_width2)

        cur_y += self.__elements_height_margin
        cur_x, cur_y = self.__min.set_coordinates(0, cur_y, remain_width)

        cur_y += self.__elements_height_margin
        cur_x, cur_y = self.__max.set_coordinates(0, cur_y, remain_width)

        cur_y += self.__elements_height_margin
        cur_x, _ = self.__sensor1_val_label.set_coordinates(0, cur_y)
        cur_x, cur_y = self.__sensor1_val.set_coordinates(cur_x + 5, cur_y)

        cur_y += self.__elements_height_margin
        cur_x, _ = self.__sensor2_val_label.set_coordinates(0, cur_y)
        cur_x, cur_y = self.__sensor2_val.set_coordinates(cur_x + 5, cur_y)

        return super().set_coordinates(x, y, width, height)

    def __pomp_pin_changed(self, event):
        self.__root.focus()

        self.__pin_changed_handler(self.__pomp_pin.get_element().get(),
                                   self.__button_pin.get_element().get(),
                                   None,
                                   None)
    
    def __button_pin_changed(self, event):
        self.__root.focus()

        self.__pin_changed_handler(self.__pomp_pin.get_element().get(),
                                   self.__button_pin.get_element().get(),
                                   None,
                                   None)
    
    def __sensor1_pin_changed(self, event):
        self.__root.focus()

        self.__pin_changed_handler(None,
                                   None,
                                   self.__sensor1_pin.get_element().get(),
                                   None)
        
    def __sensor2_pin_changed(self, event):
        self.__root.focus()
        
        self.__pin_changed_handler(None,
                                   None,
                                   None,
                                   self.__sensor2_pin.get_element().get())
    
    def get_borders(self) -> Tuple[int, int]:
        return self.__min.get_entry(), self.__max.get_entry()

    def get_name(self) -> str:
        return self.__enter_name.get_entry()

    def place(self):
        self.__control_flag = True
        
        self.__control()

        super().place()
        self.__container.place()
        
        self.__enter_name.place()

        self.__pomp_label.place()
        self.__pomp_pin.place()

        self.__sensor1_label.place()
        self.__sensor1_pin.place()
        
        self.__sensor2_label.place()
        self.__sensor2_pin.place()
        
        self.__button_label.place()
        self.__button_pin.place()

        self.__min.place()
        self.__max.place()

        self.__sensor1_val_label.place()
        self.__sensor1_val.place()

        self.__sensor2_val_label.place()
        self.__sensor2_val.place()

    def place_forget(self):
        self.__control_flag = False

        return super().place_forget()

class Pomp(Element):
    def __init__(self, root : tk.Misc):
        self.__root = root
        
        self.__container = ttk.Frame(self.__root)
        super().__init__(self.__container)

        self.__top_margin = 8
        self.__bottom_margin = 8
        self.__left_margin = 8
        self.__right_margin = 8
        self.__elements_margin = 5

        self.__settings_state = 0

        self.__pomp_pin = "GPIO1"
        self.__button_pin = "GPIO1"
        self.__bottom_sensor = "ADC0"
        self.__top_sensor = "ADC0"
        self.__min = 0
        self.__max = 0
        self.__name = "Pomp"

        self.__pomp_settings = PompSettings(self.__container, ["GPIO1", "GPIO2"], ["ADC0", "ADC1", "ADC2", "ADC3"], self.__pin_changed, self.__name, self.__read_pins, self.__change_pomp_state)

        self.__apply_pins()

        self.__create_usage()

        self.__pomp_state = 0

        self.__control_pomp()

    def __create_usage(self):
        self.__usage_container = Element(ttk.Frame(self.__container, relief="groove"))
        self.__name_label = Element(ttk.Label(self.__usage_container.get_element(), text=self.__name, background="light grey", anchor=tk.CENTER))
        
        self.__bottom_sensor_label = Element(ttk.Label(self.__usage_container.get_element(), text="Bottom sensor value =", anchor=tk.W))
        self.__bottom_sensor_value = Element(ttk.Label(self.__usage_container.get_element(), anchor=tk.W), width=23)
        self.__bottom_sensor_ratio = Element(ttk.Label(self.__usage_container.get_element(), text="<", anchor=tk.W))
        self.__min_label = Element(ttk.Label(self.__usage_container.get_element(), text="min =", anchor=tk.W))
        self.__min_value = Element(ttk.Label(self.__usage_container.get_element(), text = str(self.__min), anchor=tk.W), width=23)

        self.__top_sensor_label = Element(ttk.Label(self.__usage_container.get_element(), text="Top sensor value =", anchor=tk.W))
        self.__top_sensor_value = Element(ttk.Label(self.__usage_container.get_element(), anchor=tk.W), width=23)
        self.__top_sensor_ratio = Element(ttk.Label(self.__usage_container.get_element(), text="=", anchor=tk.W))
        self.__max_label = Element(ttk.Label(self.__usage_container.get_element(), text="max =", anchor=tk.W))
        self.__max_value = Element(ttk.Label(self.__usage_container.get_element(), text = str(self.__max), anchor=tk.W), width=23)

        self.__button_frame = Element(tk.Frame(self.__usage_container.get_element(), highlightbackground="black", highlightthickness=3))
        self.__button_frame.get_element().pack_propagate(False)

        self.__toggle_button = tk.Button(self.__button_frame.get_element(), bg="red", command=self.__toggle, bd=0)
        self.__toggle_button.pack(fill=tk.BOTH, expand=True)

    def __place_usage(self):
        self.__usage_container.place()
        self.__name_label.place()

        self.__bottom_sensor_label.place()
        self.__bottom_sensor_value.place()
        self.__bottom_sensor_ratio.place()
        self.__min_label.place()
        self.__min_value.place()

        self.__top_sensor_label.place()
        self.__top_sensor_value.place()
        self.__top_sensor_ratio.place()
        self.__max_label.place()
        self.__max_value.place()

        self.__button_frame.place()
        self.__toggle_button.pack()

    def __forget_usage(self):
        self.__usage_container.place_forget()

    def __apply_pins(self):
        print(self.__pomp_pin)
        print(self.__button_pin)
        print(self.__bottom_sensor)
        print(self.__top_sensor)
        # Здесь надо настраивать GPIO пины

    def __read_pins(self) -> Tuple[int, int, int]:
        print("__read_pins")
        # Здесь надо читать кнопку и два сенсора
        return 0, 120, 230

    def __write_pomp(self):
        # Здесь надо управлять помпой
        '''
        self.__pomp.write(self.__pomp_state)
        '''
        #self.__root.after(100, self.__write_pomp)
        return

    def __change_pomp_state(self, state):
        self.__pomp_state = state

    def __toggle(self):
        self.__pomp_state += 1
        self.__pomp_state %= 2

    def __control_pomp(self):
        btn, sens1, sens2 = self.__read_pins()

        self.__bottom_sensor_value.get_element().configure(text = str(sens1))
        self.__top_sensor_value.get_element().configure(text = str(sens2))

        if sens1 < self.__min:
            self.__bottom_sensor_ratio.get_element().configure(text="<")
        elif sens1 > self.__min:
            self.__bottom_sensor_ratio.get_element().configure(text=">")
        else:
            self.__bottom_sensor_ratio.get_element().configure(text="=")

        if sens2 < self.__max:
            self.__top_sensor_ratio.get_element().configure(text="<")
        elif sens2 > self.__max:
            self.__top_sensor_ratio.get_element().configure(text=">")
        else:
            self.__top_sensor_ratio.get_element().configure(text="=")

        if (self.__settings_state == 1):
            self.__change_pomp_state(btn)
        else:
            if sens1 <= self.__min:
                self.__pomp_state = 1
            
            if sens2 >= self.__max:
                self.__pomp_state = 0

        if(self.__pomp_state):
            self.__toggle_button.config(bg="green")
        else:
            self.__toggle_button.config(bg="red")
        
        self.__root.after(100, self.__control_pomp)

    def change_state(self, state):
        self.__settings_state = state

        if (state == 0):
            min, max = self.__pomp_settings.get_borders()
            if (min != ""):
                self.__min = int(min)
            else:
                self.__min = 0

            if (max != ""):
                self.__max = int(max)
            else:
                self.__max = 0

            print(str(self.__min), str(self.__max))

            self.__min_value.get_element().configure(text = str(self.__min))
            self.__max_value.get_element().configure(text = str(self.__max))

            name = self.__pomp_settings.get_name()
            if (name != ""):
                self.__name = name
            else:
                self.__name = "Pomp"
            self.__name_label.get_element().configure(text = self.__name)

        self.place()

    def __pin_changed(self, pomp_pin : str, button_pin : str, bottom_sensor : str, top_sensor : str):
        if (pomp_pin is not None):
            self.__pomp_pin = pomp_pin
        if (button_pin is not None):
            self.__button_pin = button_pin
        if (bottom_sensor is not None):
            self.__bottom_sensor = bottom_sensor
        if (top_sensor is not None):
            self.__top_sensor = top_sensor

        self.__apply_pins()

    def set_coordinates(self, x = 0, y = 0, width = None, height = None):
        self.__pomp_settings.set_coordinates(0, 0, width, height)

        self.__usage_container.set_coordinates(0, 0, width, height)

        remain_width = width - self.__left_margin - self.__right_margin

        cur_x = self.__left_margin
        cur_y = self.__top_margin
        _, cur_y = self.__name_label.set_coordinates(cur_x, cur_y, remain_width, 20)

        cur_y += self.__elements_margin
        cur_x, _ = self.__bottom_sensor_label.set_coordinates(cur_x, cur_y)
        cur_x, _ = self.__bottom_sensor_value.set_coordinates(cur_x, cur_y)
        cur_x, _ = self.__bottom_sensor_ratio.set_coordinates(cur_x, cur_y)
        cur_x, _ = self.__min_label.set_coordinates(cur_x, cur_y)
        cur_x, cur_y = self.__min_value.set_coordinates(cur_x, cur_y)
        
        cur_x = self.__left_margin
        cur_y += self.__elements_margin
        cur_x, _ = self.__top_sensor_label.set_coordinates(cur_x, cur_y)
        cur_x, _ = self.__top_sensor_value.set_coordinates(cur_x, cur_y)
        cur_x, _ = self.__top_sensor_ratio.set_coordinates(cur_x, cur_y)
        cur_x, _ = self.__max_label.set_coordinates(cur_x, cur_y)
        cur_x, cur_y = self.__max_value.set_coordinates(cur_x, cur_y)

        cur_x = self.__left_margin
        cur_y += self.__elements_margin
        remain_height = height - cur_y - self.__bottom_margin
        self.__button_frame.set_coordinates(cur_x, cur_y, remain_width, remain_height)

        return super().set_coordinates(x, y, width, height)
    
    def place(self):
        super().place()

        if (self.__settings_state):
            self.__forget_usage()
            self.__pomp_settings.place()
        else:
            self.__pomp_settings.place_forget()
            self.__place_usage()

class MainWindow:
    def __init__(self, root: tk.Tk):
        self.__root = root
        self.__root.title("Pomp controller")
        self.__root.state('normal')
        
        self.last_width = 0
        self.last_height = 0

        self.__root.bind('<Configure>', self.__on_resize)

        self.__root.minsize(535, 320)

        self.__top_margin = 8
        self.__bottom_margin = 8
        self.__left_margin = 8
        self.__right_margin = 8
        self.__elements_margin = 8

        self.__create_elements()

        self.__scheme_mode.set_coordinates(self.__left_margin, self.__right_margin)
        self.__scheme_mode.place()

    def __scheme_changed(self):
        self.__root.focus()
        self.__pomp1.change_state(self.__state_var.get())
        self.__pomp2.change_state(self.__state_var.get())
        return

    def __create_elements(self):
        self.__state_var = tk.IntVar(value=0)

        self.__scheme_mode_height = 20
        self.__scheme_mode = Element(ttk.Checkbutton(self.__root, text="Change scheme", variable=self.__state_var, command=self.__scheme_changed),
                                        110, self.__scheme_mode_height)
        
        self.__pomp1 = Pomp(self.__root)
        self.__pomp2 = Pomp(self.__root)
    
    def __on_resize(self, event):
        new_width = self.__root.winfo_width()
        new_height = self.__root.winfo_height()
        
        if new_width != self.last_width or new_height != self.last_height:
            self.last_width = new_width
            self.last_height = new_height

            pomp_width = (new_width - self.__left_margin - self.__right_margin - self.__elements_margin) / 2
            pomp_height = new_height - self.__top_margin - self.__scheme_mode_height - self.__elements_margin - self.__bottom_margin

            pomp_top_margin = self.__top_margin + self.__scheme_mode_height + self.__elements_margin

            cur_x, _ = self.__pomp1.set_coordinates(self.__left_margin, pomp_top_margin, pomp_width, pomp_height)
            self.__pomp1.place()

            self.__pomp2.set_coordinates(cur_x + self.__elements_margin, pomp_top_margin, pomp_width, pomp_height)
            self.__pomp2.place()

if __name__ == "__main__":
    root = tk.Tk()
    app = MainWindow(root)
    root.mainloop()