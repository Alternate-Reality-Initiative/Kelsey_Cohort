from PIL import Image

def get_pixel_rgb(image_path, x, y):
    try:
        # Open the image
        img = Image.open(image_path)

        # Ensure the image is in RGB mode
        img = img.convert("RGB")

        # Get pixel color
        rgb_value = img.getpixel((x, y))
        
        return rgb_value
    except Exception as e:
        return f"Error: {e}"
    


def remove_pixels_by_color(image_path, target_rgb, output_path):
    try:
        # Open the image
        img = Image.open(image_path).convert("RGBA")  # Ensure it has an alpha channel

        # Load pixel data
        pixels = img.load()

        # Iterate over each pixel
        width, height = img.size
        for x in range(width):
            for y in range(height):
                r, g, b, a = pixels[x, y]  # Extract RGBA values
                
                # If pixel matches the target RGB, make it transparent
                if (r, g, b) == target_rgb:
                    pixels[x, y] = (r, g, b, 0)  # Set alpha to 0 (transparent)

        # Save the modified image
        img.save(output_path)

        print(f"Processed image saved as {output_path}")

    except Exception as e:
        print(f"Error: {e}")

def print_image_size(image_path):
    img = Image.open(image_path)  # Open the image
    width, height = img.size  # Get dimensions
    print(f"Image size: {width} x {height} pixels")


# Example usage:
image_path = "Pencil_1.png"  # Change to your input image file
print_image_size(image_path)


target_rgb = (145, 145, 145)  # Change to the RGB color you want to remove (e.g., red)
output_path = "Pencil_2.png"  # Change to your desired output file name

remove_pixels_by_color(image_path, target_rgb, output_path)


    

'''
# Example usage:
image_path = "Pencil_1.png"  # Change to your image file
x, y = 3000, 1500  # Change to your desired coordinates

rgb = get_pixel_rgb(image_path, x, y)
print(f"RGB value at ({x}, {y}): {rgb}")
'''